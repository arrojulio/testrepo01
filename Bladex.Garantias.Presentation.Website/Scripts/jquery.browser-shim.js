// Shim de compatibilidad para plugins legacy (Telerik 2011).
// Restaura APIs de jQuery eliminadas en 1.9+ que jquery-migrate 3.x no cubre.
// Debe cargarse después de jquery-migrate y antes de cualquier plugin Telerik.
(function ($) {

    // === $.browser shim ===
    // Eliminado en jQuery 1.9. Telerik 2011 lo lee en telerik.common.min.js.
    if (!$.browser) {
        var ua = navigator.userAgent.toLowerCase();
        var match =
            /(msie) ([\w.]+)/.exec(ua) ||
            /(trident).*rv:([\w.]+)/.exec(ua) ||   // IE 11
            /(edge)\/([\w.]+)/.exec(ua) ||
            /(chrome)[ \/]([\w.]+)/.exec(ua) ||
            /(webkit)[ \/]([\w.]+)/.exec(ua) ||
            /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
            /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
            [];
        var browser = match[1] || '';
        $.browser = {
            msie:    browser === 'msie' || browser === 'trident',
            webkit:  browser === 'webkit' || browser === 'chrome',
            mozilla: browser === 'mozilla',
            opera:   browser === 'opera',
            version: match[2] || '0'
        };
    }

    // === $.fn.live / $.fn.die shim ===
    // Eliminados en jQuery 1.9. Telerik 2011 los usa en telerik.calendar.min.js.
    //
    // COMPORTAMIENTO ORIGINAL (jQuery 1.7):
    //   $("selector", contextElement).live(event, handler)
    //   → registra el handler en contextElement (no en document).
    //
    // Telerik llama siempre con un contexto explícito: el elemento .t-calendar.
    // Esto es crítico porque tDatePicker registra un handler directo en el mismo
    // elemento que llama stopPropagation() para evitar que el popup se cierre.
    // Si delegamos en document en lugar del contexto, ese stopPropagation bloquea
    // todos nuestros handlers antes de que lleguen a document.
    //
    // Replicando el comportamiento de jQuery 1.7 (delegar en el contexto), los
    // handlers delegados se procesan ANTES que los directos (orden de jQuery), por
    // lo que navigateDown / navigateUp / etc. se ejecutan antes de stopPropagation.
    if (!$.fn.live) {
        var _origInit = $.fn.init;

        $.fn.init = function (selector, context, root) {
            var obj = new _origInit(selector, context, root);
            if (typeof selector === 'string' && selector.charAt(0) !== '<') {
                obj._liveSelector = selector;
                // Capturar el contexto para reproducir jQuery 1.7 .live() behavior
                if (context) {
                    obj._liveContext = context.nodeType ? context
                                     : context.jquery   ? context[0]
                                     : null;
                }
            }
            return obj;
        };
        $.fn.init.prototype = $.fn;

        $.fn.live = function (types, data, fn) {
            var ctx = this._liveContext || document;
            $(ctx).on(types, this._liveSelector || '', data, fn);
            return this;
        };

        $.fn.die = function (types, fn) {
            var ctx = this._liveContext || document;
            $(ctx).off(types, this._liveSelector || '', fn);
            return this;
        };
    }

    // === $.parseJSON shim ===
    // jquery-migrate 3.x implementa $.parseJSON como JSON.parse(data + "").
    // Si data es undefined (atributo HTML ausente), se convierte al string "undefined"
    // que no es JSON válido y lanza SyntaxError. La versión original de jQuery 1.x
    // retornaba null para valores falsy. Este wrapper restaura ese comportamiento.
    if ($.parseJSON) {
        var _origParseJSON = $.parseJSON;
        $.parseJSON = function (data) {
            if (data == null || data === '') { return null; }
            return _origParseJSON.call(this, data);
        };
    }

}(jQuery));

// === Prevención de scroll en links Telerik con href="#" ===
// Listener en fase de CAPTURA: previene que el browser siga el href="#" antes
// de que jQuery procese el evento. Telerik también llama preventDefault() en sus
// handlers, pero este listener actúa de respaldo.
document.addEventListener('click', function (e) {
    var el = e.target;
    while (el && el.nodeType === 1) {
        if (el.tagName === 'A' &&
            el.getAttribute('href') === '#' &&
            el.className && el.className.indexOf('t-link') !== -1) {
            e.preventDefault();
            break;
        }
        el = el.parentNode;
    }
}, true /* capture = true */);

jQuery(function ($) {
    // Respaldo jQuery-level para cualquier t-link fuera del calendario
    $(document).on('click', 'a.t-link[href="#"]', function (e) {
        e.preventDefault();
    });

    // Fix $.telerik.trigger para jQuery 3.x:
    //
    // Telerik llama ax.stopPropagation() ANTES de $(elem).trigger(ax).
    // En jQuery 1.5.x, eso detenía la propagación del evento más allá del elemento
    // pero igual disparaba los handlers registrados en el elemento mismo.
    // En jQuery 3.x, el loop de dispatch verifica isPropagationStopped() al inicio
    // de CADA iteración — si ya es true antes de empezar, NINGÚN handler se ejecuta.
    //
    // Resultado: navigateDown llama b.trigger("change"), el change handler del
    // datepicker nunca corre, la fecha no se actualiza y el input queda sin cambio.
    //
    // Fix: reemplazar con triggerHandler (que no burbujea) y no pre-detener la propagación.
    // Esto replica el comportamiento original de jQuery 1.5: dispara handlers en el
    // elemento sin burbujear hacia arriba.
    if ($.telerik && $.telerik.trigger) {
        $.telerik.trigger = function (elem, type, data) {
            var event = new $.Event(type);
            if (data) { $.extend(event, data); }
            $(elem).triggerHandler(event);
            return event.isDefaultPrevented();
        };
    }

    // Patch $.fn.tCalendar para setear stopAnimation=true en cada instancia.
    // stopAnimation=true: fuerza actualización síncrona del DOM al navegar entre vistas.
    // Sin esto, en jQuery 3.x el nuevo contenido queda dentro del callback de
    // $.fn.animate() que puede no ejecutarse si el elemento es removido del DOM
    // antes de que la animación termine.
    // Cubrir aquí (en DOMReady, luego de que Telerik carga) asegura que tanto los
    // calendarios standalone como los popups de tDatePicker (creados de forma lazy)
    // hereden el flag al ser inicializados.
    if ($.fn.tCalendar) {
        var _origTCalendar = $.fn.tCalendar;
        $.fn.tCalendar = function (options) {
            var result = _origTCalendar.apply(this, arguments);
            this.each(function () {
                var cal = $(this).data('tCalendar');
                if (cal) { cal.stopAnimation = true; }
            });
            return result;
        };
    }

    // Cubre calendarios standalone ya inicializados al cargar la página
    setTimeout(function () {
        $('.t-calendar').each(function () {
            var cal = $(this).data('tCalendar');
            if (cal) { cal.stopAnimation = true; }
        });
    }, 0);
});
