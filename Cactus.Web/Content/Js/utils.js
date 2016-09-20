//公用js
var Utils = (function (module) {
    //js加载图片
    var imgLoad = function (url, callback, errorfunc) {
        var img = new Image();
        img.src = url;
        if (img.complete) {
            callback(img);
        } else {
            img.onload = function () {
                callback(img);
                img.onload = null;
            };
            img.onerror = function () {
                errorfunc();
            }
        };
    };
    module.ImgLoad = imgLoad;

    //js加载器
    var loadJS = function (js_url, callback) {
        var script = document.createElement('script');
        script.type = "text/javascript";
        script.src = js_url;
        script.onload = function () {
            //干你的活
        };
        var head = document.getElementsByTagName('head')[0];
        head.appendChild(script);
    };
    module.LoadJS = loadJS;

    //获取请求URL参数
    var getUrlParams = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        }
        return null;
    };
    module.GetUrlParam = function (paramName) {
        return getUrlParams(paramName);
    };


    //获取Html转义字符
    var  htmlEncode=function(html) {
        return document.createElement('a').appendChild(
               document.createTextNode(html)).parentNode.innerHTML;
    };
    module.HtmlEncode = htmlEncode;
    //获取Html 
    function htmlDecode(html) {
        var a = document.createElement('a'); a.innerHTML = html;
        return a.textContent;
    };
    module.HtmlDecode = htmlDecode;

    return module;
})(Utils || {});
/*
escape()//url编码
unescape()//url解码
*/




