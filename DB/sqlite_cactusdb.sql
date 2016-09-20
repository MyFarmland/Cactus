/*
Navicat SQLite Data Transfer

Source Server         : cactusdb
Source Server Version : 30706
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30706
File Encoding         : 65001

Date: 2016-06-13 09:45:30
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for "main"."cms_article"
-- ----------------------------
DROP TABLE "main"."cms_article";
CREATE TABLE "cms_article" (
"Article_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ColumnId"  INTEGER NOT NULL,
"Tags"  TEXT,
"ArticleContent"  CLOB NOT NULL,
"Title"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL,
"Browse"  INTEGER NOT NULL,
"Author"  TEXT,
"IsTop"  INT NOT NULL,
"IsShow"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of cms_article
-- ----------------------------
INSERT INTO "main"."cms_article" VALUES (1, 1, null, 'ArticleContent&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题', '2016-03-13 15:44:19.8146641', '2016-03-13 15:44:19.8146641', 0, '测试作者', 0, 1);
INSERT INTO "main"."cms_article" VALUES (2, 4, null, '&lt;p&gt;&lt;b&gt;求大神&lt;/b&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题1111', '2016-03-13 15:45:19.8146641', '2016-03-13 15:45:19.8146641', 0, '测试作者2222', 0, 1);
INSERT INTO "main"."cms_article" VALUES (3, 1, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题2', '2016-03-13 15:51:44.414662', '2016-03-13 15:51:44.414662', 0, '测试作者2', 0, 1);
INSERT INTO "main"."cms_article" VALUES (4, 2, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题3', '2016-03-13 15:52:09.6811072', '2016-03-13 15:52:09.6811072', 0, '测试作者3', 0, 1);
INSERT INTO "main"."cms_article" VALUES (5, 1, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题4', '2016-03-13 15:53:29.8266912', '2016-03-13 15:53:29.8266912', 0, '测试作者4', 0, 1);
INSERT INTO "main"."cms_article" VALUES (6, 1, null, '&lt;p&gt;&lt;u&gt;&lt;b&gt;测试内容&lt;/b&gt;&lt;/u&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题5', '2016-03-13 15:53:49.1027938', '2016-03-13 15:53:49.1027938', 0, '测试作者5', 0, 1);
INSERT INTO "main"."cms_article" VALUES (7, 2, null, '&lt;p&gt;&lt;i&gt;测试内容&lt;/i&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题6', '2016-03-13 15:54:18.1514552', '2016-03-13 15:54:18.1514552', 0, '测试作者6', 0, 1);
INSERT INTO "main"."cms_article" VALUES (8, 1, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题7', '2016-03-13 15:54:38.3436102', '2016-03-13 15:54:38.3436102', 0, '测试作者7', 1, 1);
INSERT INTO "main"."cms_article" VALUES (9, 2, null, '
                        &lt;p&gt;测试内容&lt;img src="/Upload/UploadFile/1457864405.jpeg" style="letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;"&gt;&lt;/p&gt;
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题8', '0001-01-01 00:00:00', '2016-03-13 18:20:07.5398916', 0, '测试作者8', 0, 0);
INSERT INTO "main"."cms_article" VALUES (10, 3, null, '
                        &lt;p&gt;测试内容&lt;img src="/Upload/UploadFile/1457864442.jpeg" style="letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;"&gt;&lt;/p&gt;
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题9', '0001-01-01 00:00:00', '2016-03-13 18:20:50.661358', 0, '测试作者9', 1, 1);
INSERT INTO "main"."cms_article" VALUES (11, 1, null, '
                        &lt;p&gt;该样板房位于中科院花桥创新服务园3号楼内，面积为41平米，层高5.9米。&lt;/p&gt;&lt;p&gt;由于室内空间受到大小的限制，设计师想把该房打造成一个三居两厅两卫的生活起居空间。设计凭借高度优势，对空间进行块面的切割。&lt;/p&gt;&lt;p&gt;&amp;nbsp;一层规划了厨房，卫生间，餐厅，客厅，卧室与茶室，二层有儿童房，主卧房，卫生间。其中每个卧室都配备了各自的书写区，包括各自的衣服存放等功能。&lt;/p&gt;&lt;p&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750086.png" class="clicked"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750132.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750203.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&amp;nbsp; 一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750278.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750306.jpeg"&gt;&lt;/p&gt;&lt;p&gt;该样板房位于中科院花桥创新服务园3号楼内，面积为41平米，层高5.9米。&lt;/p&gt;&lt;p&gt;由于室内空间受到大小的限制，设计师想把该房打造成一个三居两厅两卫的生活起居空间。设计凭借高度优势，对空间进行块面的切割。&lt;/p&gt;&lt;p&gt;&amp;nbsp;一层规划了厨房，卫生间，餐厅，客厅，卧室与茶室，二层有儿童房，主卧房，卫生间。其中每个卧室都配备了各自的书写区，包括各自的衣服存放等功能。&lt;/p&gt;&lt;p&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750086.png"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750132.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750203.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&amp;nbsp; 一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750278.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750306.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题10', '2016-03-13 15:55:52.4018461', '2016-05-07 21:24:22.973819', 0, '测试作者10', 0, 1);

-- ----------------------------
-- Table structure for "main"."cms_column"
-- ----------------------------
DROP TABLE "main"."cms_column";
CREATE TABLE "cms_column" (
"Column_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Sort"  INTEGER,
"ColumnName"  TEXT NOT NULL
);

-- ----------------------------
-- Records of cms_column
-- ----------------------------
INSERT INTO "main"."cms_column" VALUES (1, 1, '测试1');
INSERT INTO "main"."cms_column" VALUES (2, 1, '测试2');
INSERT INTO "main"."cms_column" VALUES (3, 1, '测试3');
INSERT INTO "main"."cms_column" VALUES (4, 2, '测试4');
INSERT INTO "main"."cms_column" VALUES (8, 1, '我的栏目');

-- ----------------------------
-- Table structure for "main"."cms_comment"
-- ----------------------------
DROP TABLE "main"."cms_comment";
CREATE TABLE "cms_comment" (
"Comment_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Content"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"Nickname"  TEXT NOT NULL,
"Email"  TEXT NOT NULL
);

-- ----------------------------
-- Records of cms_comment
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."cms_staticpage"
-- ----------------------------
DROP TABLE "main"."cms_staticpage";
CREATE TABLE "cms_staticpage" (
"Page_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"PageName"  TEXT NOT NULL,
"TempPageId"  TEXT NOT NULL,
"PageParameter"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL
);

-- ----------------------------
-- Records of cms_staticpage
-- ----------------------------
INSERT INTO "main"."cms_staticpage" VALUES (2, 'test', 3, '[{"key":"title","value":"标题","label":"标题","tiplabel":"标题","type":"1"},{"key":"content","value":"内容<p>1111</p>","label":"内容","type":"2"}]', '2016-05-29 23:31:43.661482', '2016-05-29 23:50:33.464836');

-- ----------------------------
-- Table structure for "main"."cms_temppage"
-- ----------------------------
DROP TABLE "main"."cms_temppage";
CREATE TABLE "cms_temppage" (
"TempPage_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TempName"  TEXT NOT NULL,
"TempByname"  TEXT,
"TempParameter"  TEXT NOT NULL,
"TempPath"  TEXT,
"TempContent"  CLOB NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL
);

-- ----------------------------
-- Records of cms_temppage
-- ----------------------------
INSERT INTO "main"."cms_temppage" VALUES (2, 'Default', '默认模板', '[{ "key": "title", "value": "标题", "label": "标题", "tiplabel": "标题", "type": "1" },{ "key": "siteDescr", "value": "描述", "label": "描述", "tiplabel": "描述", "type": "1" },{ "key": "siteKeywords", "value": "关键字", "label": "关键字", "tiplabel": "关键字", "type": "1" }, { "key": "content", "value": "内容", "label": "内容", "type": "2" }]', '/Views/Temp/Default.cshtml', '    Layout = "~/Views/Shared/_BlogLayout.cshtml";
    var obj = ViewData["Data"] as Newtonsoft.Json.Linq.JObject;
    ViewBag.Title = obj["title"].ToString();
    ViewBag.SiteDescr = obj["siteDescr"].ToString();
    ViewBag.SiteKeywords = obj["siteKeywords"].ToString();
}


<form class="pure-form pure-search pure-g" action="@Url.Action("Search", "Blog")" method="get">
    <input type="text" placeholder="输入你的关键字">
    <button type="submit" class="pure-button pure-button-primary">搜索</button>
</form>
<div class="posts">

    <section class="post">
        <header class="post-header">
            <h2 class="post-title">@(obj["title"].ToString())</h2>
            <p class="post-meta">
                发表于 @(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                

            </p>
        </header>

        <div class="post-description-detail">
            @Html.Raw(obj["content"].ToString())
        </div>
    </section>
</div>', '2016-05-29 17:53:42.9683307', '2016-05-29 21:46:23.6583591');
INSERT INTO "main"."cms_temppage" VALUES (3, 'Default2', '默认模板2', '[{ "key": "title", "value": "标题", "label": "标题", "tiplabel": "标题", "type": "1" }, { "key": "content", "value": "内容", "label": "内容", "type": "2" }]', '/Views/Temp/Default2.cshtml', '@{
    Layout = "~/Views/Shared/_BlogLayout.cshtml";
    var obj = ViewData["Data"] as Newtonsoft.Json.Linq.JObject;
}
<form class="pure-form pure-search pure-g" action="@Url.Action("Search", "Blog")" method="get">
    <input type="text" placeholder="输入你的关键字">
    <button type="submit" class="pure-button pure-button-primary">搜索</button>
</form>
<div class="posts">

    <section class="post">
        <header class="post-header">
            <h2 class="post-title">@(obj["title"].ToString())</h2>
            <p class="post-meta">
                发表于 @(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            </p>
        </header>

        <div class="post-description-detail">
            @Html.Raw(obj["content"].ToString())
        </div>
    </section>
</div>', '2016-05-29 21:36:42.6445554', '2016-05-29 21:48:16.9302073');

-- ----------------------------
-- Table structure for "main"."sqlite_sequence"
-- ----------------------------
DROP TABLE "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_role', 4);
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_user', 3190);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_column', 12);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_article', 12);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_item', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_category', 15);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_member', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_order', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_staticpage', 2);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_temppage', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_comment', 0);

-- ----------------------------
-- Table structure for "main"."store_category"
-- ----------------------------
DROP TABLE "main"."store_category";
CREATE TABLE "store_category" (
"Category_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"CategoryName"  TEXT NOT NULL,
"GroupCode"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of store_category
-- ----------------------------
INSERT INTO "main"."store_category" VALUES (4, '床', 1);
INSERT INTO "main"."store_category" VALUES (8, '餐桌椅', 1);
INSERT INTO "main"."store_category" VALUES (11, '电视柜', 1);
INSERT INTO "main"."store_category" VALUES (12, '沙发', 1);
INSERT INTO "main"."store_category" VALUES (13, '书桌', 1);
INSERT INTO "main"."store_category" VALUES (14, '梳妆台', 1);
INSERT INTO "main"."store_category" VALUES (15, '衣柜', 1);

-- ----------------------------
-- Table structure for "main"."store_item"
-- ----------------------------
DROP TABLE "main"."store_item";
CREATE TABLE "store_item" (
"Item_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Item_Img"  TEXT,
"Title"  TEXT NOT NULL,
"CategoryId"  INTEGER NOT NULL,
"Content"  CLOB NOT NULL,
"Price"  INTEGER NOT NULL,
"Stock"  INTEGER NOT NULL,
"AddTime"  DATETIME NOT NULL,
"State"  INTEGER NOT NULL,
"Flag"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of store_item
-- ----------------------------
INSERT INTO "main"."store_item" VALUES (1, '/Upload/Item/1459220656.jpg', '测试标题', 4, '
                         
                         
                         111111
                    &lt;p&gt;&lt;img src="/Upload/ItemContent/1461855443.jpeg" style="max-width:100%;"&gt;&lt;br&gt;&lt;/p&gt;
                    
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', 100, 992, '2016-04-02 10:26:13.8261764', 2, 1);
INSERT INTO "main"."store_item" VALUES (3, '/Upload/Item/1459304651.jpg', '测试标题3', 4, '
                         
                         
                         &lt;p&gt;请问请问&lt;/p&gt;
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;
                    
                    ', 115, 10, '2016-03-30 10:24:14.1246229', 1, 0);

-- ----------------------------
-- Table structure for "main"."store_member"
-- ----------------------------
DROP TABLE "main"."store_member";
CREATE TABLE "store_member" (
"Member_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"LoginName"  TEXT NOT NULL,
"Password"  TEXT NOT NULL,
"AddTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL,
"AvatarPath"  TEXT NOT NULL,
"Phone"  TEXT,
"State"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of store_member
-- ----------------------------
INSERT INTO "main"."store_member" VALUES (1, 'test', 'cebfd1559b68d67688884d7a3d3e8c', '2016-03-13 15:44:19.8146641', '2016-04-30 20:22:39.1496908', '/Upload/Avatar/20160430202223442.jpg', 188, 1);
INSERT INTO "main"."store_member" VALUES (2, 'test01', 'd2f278e251d1566edd72846d4ad97f', '2016-04-02 21:05:36.7747662', '2016-04-02 21:05:36.7747662', '/Upload/Sys/Avatar.jpg', 188, 1);
INSERT INTO "main"."store_member" VALUES (3, 'test02', 'd2f278e251d1566edd72846d4ad97f', '2016-04-02 21:05:59.1930485', '2016-04-24 23:11:31.7194065', '/Upload/Sys/Avatar.jpg', 188, 1);

-- ----------------------------
-- Table structure for "main"."store_order"
-- ----------------------------
DROP TABLE "main"."store_order";
CREATE TABLE "store_order" (
"Order_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ItemId"  INTEGER NOT NULL,
"Price"  INTEGER NOT NULL,
"Num"  INTEGER NOT NULL,
"Des"  TEXT NOT NULL,
"Address"  TEXT NOT NULL,
"Phone"  TEXT NOT NULL,
"State"  INTEGER NOT NULL,
"AddTime"  DATETIME NOT NULL,
"IsDeliver"  INTEGER,
"TrackingNumber"  TEXT,
"DeliverTime"  DATETIME,
"IsReceipt"  INTEGER,
"ReceiptTime"  DATETIME,
"Comment"  TEXT,
"MemberId"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of store_order
-- ----------------------------
INSERT INTO "main"."store_order" VALUES (1, 3, 10, 3, 1111, 1111, 1111, 2, '2016-04-02 10:26:13.8261764', 1, 1111111, '2016-04-03 17:07:50.6400387', null, null, null, 3);
INSERT INTO "main"."store_order" VALUES (2, 3, 10, 1, 1111, 1111, 1111, 3, '2016-04-02 10:26:14.8261764', 1, 11111111111111, '2016-04-24 17:15:02.4730116', 1, '2016-04-24 23:16:13.0324967', 111111, 3);
INSERT INTO "main"."store_order" VALUES (3, 1, 100, 3, 11111111111111, 111111, 11111111, 3, '2016-04-24 17:05:29.5502423', 1, 645464645, '2016-04-25 23:19:23.0670735', 1, '2016-04-25 23:19:35.2907727', 4646464, 1);

-- ----------------------------
-- Table structure for "main"."sys_role"
-- ----------------------------
DROP TABLE "main"."sys_role";
CREATE TABLE "sys_role" (
"Role_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"RoleName"  TEXT NOT NULL,
"ActionIds"  TEXT NOT NULL
);

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO "main"."sys_role" VALUES (1, '管理员', null);
INSERT INTO "main"."sys_role" VALUES (3, '测试角色', 'Blog_01,Blog_102,Root_01,Root_03,Root_04,Root_05,Root_06');

-- ----------------------------
-- Table structure for "main"."sys_user"
-- ----------------------------
DROP TABLE "main"."sys_user";
CREATE TABLE "sys_user" (
"User_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"RoleId"  INT,
"Name"  TEXT(255),
"Password"  TEXT,
"NickName"  TEXT,
"Avatar"  TEXT,
"Email"  TEXT,
"Phone"  TEXT,
"Qq"  TEXT,
"AddTime"  DATETIME,
"LastLoginTime"  DATETIME,
"LastLoginIp"  TEXT,
"IsSuperUser"  INT
);

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO "main"."sys_user" VALUES (1, 1, '702295399@qq.com', '61206e4c95bf3057999b3485dd1a63', '漫漫洒洒', '/Upload/Avatar/Avatar_1.jpg', '702295399@qq.com', 1888888889, 702295399, '2016-02-07 09:44:53.2348994', '2016-06-05 00:43:05.2531161', '127.0.0.1', 1);
INSERT INTO "main"."sys_user" VALUES (3190, 3, '测试账户', 'cebfd1559b68d67688884d7a3d3e8c', '测试小二', '/Upload/Avatar/Avatar_3190.jpg', '7022953991@qq.com', 1234567891, 1234567891, '2016-02-19 10:26:13.8261764', '2016-06-04 20:24:10.6925901', '127.0.0.1', 0);
