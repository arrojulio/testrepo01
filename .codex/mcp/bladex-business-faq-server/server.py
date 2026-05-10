#!/usr/bin/env python
"""
Bladex Garantias Business FAQ MCP server.

Minimal MCP stdio server with no third-party dependencies. It exposes a small
curated knowledge base for frequent business questions about Bladex Garantias.
"""

import json
import sys
from typing import Any, Dict, List


SERVER_NAME = "bladex-business-faq"
SERVER_VERSION = "0.1.0"


FAQ_TOPICS: Dict[str, Dict[str, Any]] = {
    "garantias": {
        "title": "Garantias",
        "keywords": ["garantia", "garantias", "garantia financiera", "garantias financieras", "colateral"],
        "answer": (
            "Una Garantia representa el respaldo financiero o juridico asociado a una operacion. "
            "En el sistema, todas las garantias heredan de GarantiaBase y comparten datos comunes "
            "como cliente, moneda, pais, estado, fechas, valuacion y trazabilidad operativa. "
            "El objetivo de negocio es registrar, consultar y mantener garantias con control de estados "
            "y aprobacion para operaciones sensibles."
        ),
    },
    "tipos_de_garantia": {
        "title": "Tipos de Garantia",
        "keywords": [
            "tipo",
            "tipos",
            "contrato",
            "deposito",
            "deposito otro banco",
            "inmueble",
            "mueble",
            "prenda",
            "otra",
        ],
        "answer": (
            "Los tipos principales son GarantiaContrato, GarantiaDeposito, "
            "GarantiaDepositoOtroBanco, GarantiaInmueble, GarantiaMueble, GarantiaPrenda "
            "y GarantiaOtra. Todos comparten la base GarantiaBase, pero cada subtipo agrega "
            "campos y reglas propias segun la naturaleza del respaldo: contrato, deposito, "
            "bien inmueble, bien mueble, prenda u otro instrumento."
        ),
    },
    "makerchecker": {
        "title": "MakerChecker",
        "keywords": ["makerchecker", "maker", "checker", "aprobacion", "aprobar", "rechazar", "pendiente"],
        "answer": (
            "MakerChecker es el flujo de doble control para operaciones sensibles. "
            "Un usuario maker propone o ejecuta una operacion y un usuario checker la revisa. "
            "El checker puede aprobar o rechazar segun el cambio, el estado de la garantia y "
            "las reglas de auditoria. Este flujo reduce riesgo operativo y evita que cambios "
            "criticos queden efectivos sin revision independiente."
        ),
    },
    "aval": {
        "title": "Aval",
        "keywords": ["aval", "avales", "endorsement", "garante"],
        "answer": (
            "Un Aval representa un respaldo, garante o endoso asociado a una garantia. "
            "El subsistema AvalManager centraliza la administracion de avales y permite "
            "relacionarlos con garantias cuando el negocio requiere identificar terceros "
            "que respaldan una obligacion."
        ),
    },
    "cliente": {
        "title": "Cliente",
        "keywords": ["cliente", "clientes", "deudor", "garante", "obligado"],
        "answer": (
            "Cliente representa la parte de negocio vinculada a la garantia. "
            "Las garantias suelen consultarse, filtrarse o mantenerse por cliente, y el sistema "
            "usa mecanismos de busqueda/autocomplete para ubicar clientes sin cargar listas "
            "completas en formularios operativos."
        ),
    },
    "estados": {
        "title": "Estados",
        "keywords": ["estado", "estados", "status", "internalstatus", "activo", "bloqueado", "eliminado"],
        "answer": (
            "El sistema distingue estados de negocio y estados internos. Status describe la "
            "condicion funcional de la garantia, mientras InternalStatus controla estados "
            "operativos como activo, bloqueado o eliminado. MakerChecker puede bloquear una "
            "garantia mientras una operacion esta pendiente para evitar cambios concurrentes "
            "o inconsistentes."
        ),
    },
    "referencias": {
        "title": "Datos de Referencia",
        "keywords": ["pais", "paises", "moneda", "monedas", "banco", "bancos", "aseguradora", "referencia"],
        "answer": (
            "Pais, Monedas, Bancos, Aseguradoras, Frecuencias y otras entidades similares son "
            "datos de referencia. Se usan para clasificar y completar garantias. Por performance, "
            "estos datos suelen ser buenos candidatos para cache cuando se consultan con mucha "
            "frecuencia y cambian poco."
        ),
    },
    "importsync": {
        "title": "ImportSync",
        "keywords": ["import", "importsync", "sync", "sincronizacion", "atomos", "fecha de corte"],
        "answer": (
            "ImportSync es el modulo de integracion que importa o sincroniza datos externos hacia "
            "el dominio de Garantias. Trabaja con fechas de corte y registros fuente para mantener "
            "la informacion operativa alineada con sistemas externos."
        ),
    },
    "arquitectura": {
        "title": "Arquitectura de Negocio y Capas",
        "keywords": ["arquitectura", "capas", "ddd", "dominio", "repositorio", "servicio"],
        "answer": (
            "El sistema sigue una arquitectura DDD por capas. Presentation contiene MVC y vistas; "
            "Application orquesta casos de uso; DomainModel contiene entidades, servicios y contratos; "
            "Infrastructure e Infrastructure.Repositories implementan cache, logging y acceso SQL. "
            "La capa Domain no debe depender de Infrastructure ni de frameworks externos."
        ),
    },
}


def response_result(request_id: Any, result: Dict[str, Any]) -> Dict[str, Any]:
    return {"jsonrpc": "2.0", "id": request_id, "result": result}


def response_error(request_id: Any, code: int, message: str) -> Dict[str, Any]:
    return {"jsonrpc": "2.0", "id": request_id, "error": {"code": code, "message": message}}


def text_content(text: str) -> List[Dict[str, str]]:
    return [{"type": "text", "text": text}]


def normalize(text: str) -> str:
    replacements = {
        "á": "a",
        "é": "e",
        "í": "i",
        "ó": "o",
        "ú": "u",
        "ñ": "n",
        "ü": "u",
    }
    lowered = text.lower()
    for source, target in replacements.items():
        lowered = lowered.replace(source, target)
    return lowered


def find_matching_topics(question: str) -> List[Dict[str, Any]]:
    normalized_question = normalize(question)
    matches: List[Dict[str, Any]] = []

    for topic_id, topic in FAQ_TOPICS.items():
        score = 0
        if normalize(topic_id) in normalized_question:
            score += 3
        for keyword in topic["keywords"]:
            if normalize(keyword) in normalized_question:
                score += 1
        if score:
            matches.append({"id": topic_id, "score": score, "topic": topic})

    return sorted(matches, key=lambda item: item["score"], reverse=True)


def answer_business_question(question: str) -> str:
    if not question or not question.strip():
        return (
            "Indica una pregunta de negocio sobre Garantias, MakerChecker, Aval, Cliente, "
            "estados, datos de referencia o ImportSync."
        )

    matches = find_matching_topics(question)
    if not matches:
        topics = ", ".join(topic["title"] for topic in FAQ_TOPICS.values())
        return (
            "No encontre una respuesta curada para esa pregunta. "
            "Temas disponibles: {0}. Si la pregunta depende de datos actuales de la base "
            "BLX_GARANTIAS, conviene consultar SQL Server o el repositorio correspondiente."
        ).format(topics)

    best = matches[0]["topic"]
    related = [item["topic"]["title"] for item in matches[1:4]]
    answer = "{0}: {1}".format(best["title"], best["answer"])
    if related:
        answer += "\n\nTemas relacionados: {0}.".format(", ".join(related))
    return answer


def list_business_topics() -> str:
    lines = ["Temas de negocio cubiertos:"]
    for topic_id, topic in FAQ_TOPICS.items():
        lines.append("- {0}: {1}".format(topic_id, topic["title"]))
    return "\n".join(lines)


def handle_initialize(request_id: Any) -> Dict[str, Any]:
    return response_result(
        request_id,
        {
            "protocolVersion": "2024-11-05",
            "capabilities": {"tools": {}},
            "serverInfo": {"name": SERVER_NAME, "version": SERVER_VERSION},
        },
    )


def handle_tools_list(request_id: Any) -> Dict[str, Any]:
    return response_result(
        request_id,
        {
            "tools": [
                {
                    "name": "answer_business_question",
                    "description": "Responde preguntas frecuentes de negocio sobre Bladex Garantias.",
                    "inputSchema": {
                        "type": "object",
                        "properties": {
                            "question": {
                                "type": "string",
                                "description": "Pregunta de negocio en espanol o ingles.",
                            }
                        },
                        "required": ["question"],
                    },
                },
                {
                    "name": "list_business_topics",
                    "description": "Lista los temas de negocio cubiertos por este MCP server.",
                    "inputSchema": {"type": "object", "properties": {}},
                },
            ]
        },
    )


def handle_tools_call(request_id: Any, params: Dict[str, Any]) -> Dict[str, Any]:
    tool_name = params.get("name")
    arguments = params.get("arguments") or {}

    if tool_name == "answer_business_question":
        question = str(arguments.get("question", ""))
        return response_result(request_id, {"content": text_content(answer_business_question(question))})

    if tool_name == "list_business_topics":
        return response_result(request_id, {"content": text_content(list_business_topics())})

    return response_error(request_id, -32601, "Unknown tool: {0}".format(tool_name))


def handle_request(request: Dict[str, Any]) -> Dict[str, Any]:
    request_id = request.get("id")
    method = request.get("method")
    params = request.get("params") or {}

    if method == "initialize":
        return handle_initialize(request_id)
    if method == "tools/list":
        return handle_tools_list(request_id)
    if method == "tools/call":
        return handle_tools_call(request_id, params)
    if method == "notifications/initialized":
        return {}

    return response_error(request_id, -32601, "Method not found: {0}".format(method))


def main() -> None:
    for line in sys.stdin:
        if not line.strip():
            continue

        try:
            request = json.loads(line)
            response = handle_request(request)
        except Exception as ex:
            response = response_error(None, -32603, "Internal error: {0}".format(ex))

        if response:
            sys.stdout.write(json.dumps(response, separators=(",", ":")) + "\n")
            sys.stdout.flush()


if __name__ == "__main__":
    main()
