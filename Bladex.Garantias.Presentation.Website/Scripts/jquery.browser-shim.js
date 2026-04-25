// Shim minimal de $.browser para compatibilidad con plugins legacy (Telerik 2011, bgiframe).
// $.browser fue eliminado en jQuery 1.9. Este shim restaura la superficie mínima requerida.
(function ($) {
    if ($.browser) { return; }
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
    var browser = match[1] || "";
    var version = match[2] || "0";
    $.browser = {
        msie:    browser === "msie" || browser === "trident",
        webkit:  browser === "webkit" || browser === "chrome",
        mozilla: browser === "mozilla",
        opera:   browser === "opera",
        version: version
    };
}(jQuery));
