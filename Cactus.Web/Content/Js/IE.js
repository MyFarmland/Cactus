function IsIE8low() {
    var browser = navigator.appName
    var b_version = navigator.appVersion
    var version = b_version.split(";");  
    var IE8 = false;
    if (version.length < 2) {
        return IE8;
    }
    var trim_Version = version[1].replace(/[ ]/g, "");
    if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE6.0") { IE8 = true; }
    else if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") { IE8 = true; }
    else if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") { IE8 = true; }
    return IE8;
}
$(function () {
    if (IsIE8low()) {
        alert("请使用高版本浏览器，建议使用火狐、Chrome、IE9");
    }
})