# Bladex Business FAQ MCP Server

Servidor MCP local para responder preguntas frecuentes de negocio del sistema
Bladex Garantias.

## Que cubre

- Garantias y tipos de garantia
- MakerChecker
- Aval
- Cliente
- Estados de negocio e internos
- Datos de referencia
- ImportSync
- Arquitectura de capas desde el punto de vista de negocio

## Ejecucion directa

```powershell
python .codex/mcp/bladex-business-faq-server/server.py
```

El servidor habla JSON-RPC por `stdio`, como espera un cliente MCP.

## Configuracion MCP

Ejemplo para un cliente que soporte `mcpServers`:

```json
{
  "mcpServers": {
    "bladex-business-faq": {
      "command": "python",
      "args": [
        "C:/dev/ap.elcazal/testrepo01/.codex/mcp/bladex-business-faq-server/server.py"
      ]
    }
  }
}
```

## Tools

### `answer_business_question`

Responde una pregunta frecuente de negocio.

Ejemplo de argumentos:

```json
{
  "question": "Que es MakerChecker?"
}
```

### `list_business_topics`

Lista los temas cubiertos por la base de conocimiento del servidor.

## Limites

Este servidor responde con conocimiento curado del proyecto. No consulta SQL Server
ni lee datos actuales de `BLX_GARANTIAS`. Para preguntas sobre registros reales,
montos, clientes existentes o estados actuales de una garantia, debe usarse una
consulta contra la base de datos o un MCP server adicional conectado a SQL Server.
