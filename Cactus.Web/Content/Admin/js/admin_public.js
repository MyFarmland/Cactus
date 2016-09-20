//admin公用js
var Admin = (function (module) {
    var loadObj = null;
    var loadDialog_show = function () {
        loadObj = layer.load(1,{
            shade: [0.5, '#000'] //0.1透明度的白色背景
        });
    };
    module.loadDialog_show = loadDialog_show;
    var loadDialog_hide = function () {
        layer.close(loadObj);
    };
    module.loadDialog_hide = loadDialog_hide;
    var history_page = new Array();
    var index_page = new Array();;
    var loadContent_Jq = function (id, url) {
        loadDialog_show();
        //根据load源码修改
        var selector,off = url.indexOf(" ");
        if (off >= 0) {
            selector = url.slice(off, url.length);
            url = url.slice(0, off);
        }
        $.ajax({
            url: url,
            type: "GET",
            dataType: "html",
            complete: function (xhr, status, responseText) {
                responseText = xhr.responseText;
                var type = xhr.getResponseHeader("Content-Type");
                type = type.toLowerCase();
                if (type.indexOf('text/html')>=0) {
                    $("#" + id).html(selector ? jQuery("<div>").append(responseText.replace(rscript, "")).find(selector) : responseText);

                } else {
                    var data = window.eval('(' + responseText + ')');
                    layer.alert(data.msg);
                }
                if (url != "") {
                    var index_str = "";
                    if (index_page.length > 0) {
                        index_str = index_page.pop();//取出原先的页
                        console.log('history push:' + index_str);
                        history_page.push(index_str);//加入历史 unshift push
                    }
                    index_page.push(url);//加入新的页
                }
                loadDialog_hide();
            }
        })
        //load的原理就是ajax，为了判断type现在弃用
        //$("#" + id).load(url, function () {
        //    loadDialog_hide();
        //});
    }
    module.loadContent_Jq = loadContent_Jq;
    var loadBackPage = function (id) {
        if (history_page.length > 0) {
            index_page.pop();
            var history = history_page.pop();
            console.log('history pop:'+history);
            loadContent_Jq(id, history);
        } else { layer.alert('没有前一页')}
    }
    module.loadBackPage = loadBackPage;
    module.history_page = history_page;
    var loadRefresh = function (id) {
        if (index_page.length > 0) {
            var index_str = index_page.pop();
            loadContent_Jq(id, index_str);
        }
    };
    module.loadRefresh = loadRefresh;
    var tipTable = function (jq_obj, msg) {
        console.log('tipTable');
        var head = jq_obj.find('thead tr th');
        var tbody = jq_obj.find('tbody');
        tbody.html("");
        //无记录
        var tr01 = $("<tr align=\"center\"></tr>");
        $("<td colspan=\"" + head.length + "\">" + msg + "</td>").appendTo(tr01);
        tbody.append(tr01);
    }
    module.tipTable = tipTable;
    //自动表格初始
    //jq_obj:jquery表格对象
    //rows:数据集合
    //columns:字段集合，根据字段处理数据，格式[{field: "Id", formatter:function(val, rec){}}]，val：数据，rec：序号
    var renderTable = function (jq_obj, rows, columns) {
        var thead = jq_obj.find('thead');
        if (thead[0] == undefined) {
            jq_obj.append('<thead></thead>');
            thead = jq_obj.find('thead');
            var tr = $("<tr></tr>");
            for (var j = 0; j < columns.length; j++) {
                var th_title = columns[j].title;
                $("<th>" + th_title + "</th>").appendTo(tr);
            }
            thead.append(tr);
        }
        var tbody = jq_obj.find('tbody');
        if (tbody[0] == undefined) {
            jq_obj.append('<tbody></tbody>');
            tbody = jq_obj.find('tbody');
        }
        tbody.html("");
        var head = jq_obj.find('thead tr th');
        if (rows[0] != undefined && rows[0] != null) {
            for (var i = 0; i < rows.length; i++) {
            var r = rows[i];
            var tr = $("<tr></tr>");
            for (var j = 0; j < columns.length; j++) {
                var fieldstr = columns[j].field;
                var valstr = r[fieldstr];
                if (valstr == undefined || valstr == null) {
                    if (fieldstr.indexOf('.') != -1) {
                        var arr = fieldstr.split(".");
                        switch (arr.length) {
                            case 2:
                                valstr = r[arr[0]][arr[1]];
                                break;
                            case 3:
                                valstr = r[arr[0]][arr[1]][arr[2]];
                                break;
                            case 4:
                                valstr = r[arr[0]][arr[1]][arr[2]][arr[3]];
                                break;
                            default:
                                valstr = r[arr[0]][arr[1]];
                        }
                    }
                }

                if (columns[j].formatter != undefined && typeof columns[j].formatter === 'function') {
                    valstr = columns[j].formatter(valstr, i,r);
                }
                $("<td>" + valstr + "</td>").appendTo(tr);
            }
            tbody.append(tr);
            }
        } else {
            //无记录
            var tr = $("<tr align=\"center\"></tr>");
            $("<td colspan=\"" + head.length + "\">(暂无相关记录)</td>").appendTo(tr);
            tbody.append(tr);
        }
    }
    module.renderTable = renderTable;

    var tabInit = function (defaultIndex) {
        var $tabButtons = $('.pure-tab .pure-tab-head .pure-tab-button');
        $tabButtons.on('click', function () {
            selectTab(this);
        })
        function selectTab(obj) {
            var index = $tabButtons.index($(obj));
            $tabButtons.removeClass('pure-tab-active');
            $(obj).addClass('pure-tab-active');
            $('.pure-tab .pure-tab-content').hide();
            var select = $('.pure-tab .pure-tab-content').eq(index);
            select.show();
        }
        selectTab($tabButtons.eq(defaultIndex));
    }
    module.tabInit = tabInit;
    return module;
})(Admin || {});