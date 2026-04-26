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
    // Parchea $.fn.init para capturar el string selector original (jQuery 3.x eliminó
    // this.selector), luego delega via $(document).on(event, selector, handler).
    if (!$.fn.live) {
        var _origInit = $.fn.init;

        $.fn.init = function (selector, context, root) {
            var obj = new _origInit(selector, context, root);
            if (typeof selector === 'string') {
                obj._liveSelector = selector;
            }
            return obj;
        };
        $.fn.init.prototype = $.fn;

        $.fn.live = function (types, data, fn) {
            $(document).on(types, this._liveSelector || '', data, fn);
            return this;
        };

        $.fn.die = function (types, fn) {
            $(document).off(types, this._liveSelector || '', fn);
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
// Doble estrategia:
//   A) Listener en fase de CAPTURA (nativo, precede a cualquier handler jQuery bubble).
//      Previene el salto al inicio de la página independientemente del orden en que
//      se ejecuten los handlers delegados de $.fn.live.
//   B) setTimeout(0) después del DOMReady para parchear instancias de tCalendar:
//      - Activa stopAnimation=true (actualización síncrona del DOM, sin callback de animate).
//      - Enlaza handlers de click directamente en el contenedor de cada calendario,
//        lo que complementa (y sirve de respaldo a) la delegación vía $.fn.live.

document.addEventListener('click', function (e) {
    var el = e.target;
    // Asciende hasta encontrar el <a> (el clic puede caer sobre el <span> interior)
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
    // Fallback jQuery-level prevention (para cualquier t-link que no sea Telerik nav)
    $(document).on('click', 'a.t-link[href="#"]', function (e) {
        e.preventDefault();
    });

    // Después de que todos los handlers DOMReady de Telerik hayan corrido
    // (incluyendo la inicialización del widget tCalendar), parchear las instancias.
    setTimeout(function () {
        $('.t-calendar').each(function () {
            var $cal = $(this);
            var cal  = $cal.data('tCalendar');
            if (!cal) { return; }

            // Fuerza actualización síncrona del DOM al navegar entre vistas.
            // Sin esto, el nuevo contenido se inserta dentro del callback de
            // $.fn.animate(), que en jQuery 3.x puede no ejecutarse si el
            // elemento animado ya no está en el DOM al finalizar la animación.
            cal.stopAnimation = true;

            // Binding directo en el contenedor del calendario para cada botón
            // de navegación. Actúa como respaldo a la delegación $.fn.live en document.
            $cal.on('click.telerik-shim', '.t-nav-fast:not(.t-state-disabled)',
                function (e) { e.preventDefault(); cal.navigateUp(e); });

            $cal.on('click.telerik-shim', '.t-nav-prev:not(.t-state-disabled)',
                function (e) { e.preventDefault(); cal.navigateToPast(e); });

            $cal.on('click.telerik-shim', '.t-nav-next:not(.t-state-disabled)',
                function (e) { e.preventDefault(); cal.navigateToFuture(e); });
        });
    }, 0);
});
