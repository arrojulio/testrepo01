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
// Telerik 2011 usa <a href="#"> para los botones de navegación del calendario
// (prev, fast/zoom-out, next). Este handler garantiza que un click en cualquiera
// de esos links nunca cause un salto al inicio de la página, independientemente
// de si el handler delegado de $.fn.live logra capturar el evento primero.
jQuery(function ($) {
    $(document).on('click', 'a.t-link[href="#"]', function (e) {
        e.preventDefault();
    });
});
