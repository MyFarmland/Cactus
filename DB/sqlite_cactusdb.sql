/*
Navicat SQLite Data Transfer

Source Server         : cactusdb
Source Server Version : 30706
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30706
File Encoding         : 65001

Date: 2016-12-18 18:35:30
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for "main"."cms_article"
-- ----------------------------
DROP TABLE "main"."cms_article";
CREATE TABLE "cms_article" (
"Article_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ColumnId"  INTEGER NOT NULL,
"Tagids"  TEXT,
"Tags"  TEXT,
"ArticleContent"  CLOB NOT NULL,
"Title"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL,
"Browse"  INTEGER NOT NULL,
"Author"  TEXT,
"ImgUrl"  TEXT,
"Digest"  TEXT,
"SEO_Title"  TEXT,
"SEO_Keywords"  TEXT,
"SEO_DES"  TEXT,
"IsTop"  INT NOT NULL,
"IsShow"  INTEGER NOT NULL,
"Source"  TEXT,
"SourceLink"  TEXT,
"Praise"  INTEGER DEFAULT 0
);

-- ----------------------------
-- Records of cms_article
-- ----------------------------
INSERT INTO "main"."cms_article" VALUES (1, 1, null, null, 'ArticleContent&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题', '2016-03-13 15:44:19.8146641', '2016-03-13 15:44:19.8146641', 0, '测试作者', null, null, null, null, null, 0, 1, null, null, 5);
INSERT INTO "main"."cms_article" VALUES (2, 4, null, null, '&lt;p&gt;&lt;b&gt;求大神&lt;/b&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题1111', '2016-03-13 15:45:19.8146641', '2016-03-13 15:45:19.8146641', 0, '测试作者2222', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (3, 1, null, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题2', '2016-03-13 15:51:44.414662', '2016-03-13 15:51:44.414662', 0, '测试作者2', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (4, 2, null, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题3', '2016-03-13 15:52:09.6811072', '2016-03-13 15:52:09.6811072', 0, '测试作者3', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (5, 1, null, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题4', '2016-03-13 15:53:29.8266912', '2016-03-13 15:53:29.8266912', 0, '测试作者4', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (7, 2, null, null, '&lt;p&gt;&lt;i&gt;测试内容&lt;/i&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题6', '2016-03-13 15:54:18.1514552', '2016-03-13 15:54:18.1514552', 0, '测试作者6', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (8, 1, null, null, '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题7', '2016-03-13 15:54:38.3436102', '2016-03-13 15:54:38.3436102', 0, '测试作者7', null, null, null, null, null, 1, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (9, 2, null, null, '
                        &lt;p&gt;测试内容&lt;img src="/Upload/UploadFile/1457864405.jpeg" style="letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;"&gt;&lt;/p&gt;
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题8', '0001-01-01 00:00:00', '2016-03-13 18:20:07.5398916', 0, '测试作者8', null, null, null, null, null, 0, 0, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (10, 3, null, null, '
                        &lt;p&gt;测试内容&lt;img src="/Upload/UploadFile/1457864442.jpeg" style="letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;"&gt;&lt;/p&gt;
                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题9', '0001-01-01 00:00:00', '2016-03-13 18:20:50.661358', 0, '测试作者9', null, null, null, null, null, 1, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (11, 1, '1,3,9', 'Test,test,test6', '
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        &lt;p&gt;该样板房位于中科院花桥创新服务园3号楼内，面积为41平米，层高5.9米。&lt;/p&gt;&lt;p&gt;由于室内空间受到大小的限制，设计师想把该房打造成一个三居两厅两卫的生活起居空间。设计凭借高度优势，对空间进行块面的切割。&lt;/p&gt;&lt;p&gt;&amp;nbsp;一层规划了厨房，卫生间，餐厅，客厅，卧室与茶室，二层有儿童房，主卧房，卫生间。其中每个卧室都配备了各自的书写区，包括各自的衣服存放等功能。&lt;/p&gt;&lt;p&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750086.png" class="clicked"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750132.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750203.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&amp;nbsp; 一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750278.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750306.jpeg"&gt;&lt;/p&gt;&lt;p&gt;该样板房位于中科院花桥创新服务园3号楼内，面积为41平米，层高5.9米。&lt;/p&gt;&lt;p&gt;由于室内空间受到大小的限制，设计师想把该房打造成一个三居两厅两卫的生活起居空间。设计凭借高度优势，对空间进行块面的切割。&lt;/p&gt;&lt;p&gt;&amp;nbsp;一层规划了厨房，卫生间，餐厅，客厅，卧室与茶室，二层有儿童房，主卧房，卫生间。其中每个卧室都配备了各自的书写区，包括各自的衣服存放等功能。&lt;/p&gt;&lt;p&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="file:///C:/users/administrator/appdata/roaming/360se6/User%20Data/temp/6fbdaedbb62dee1c7ef68031e3196cb7.jpg_w660"&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750086.png"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750132.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750203.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&amp;nbsp; 一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。一层茶室区具备了多种功能性，白天可将电动桌升起，为家庭生活提供了休闲品茶区，并且利用了地台的高度，在茶室周边范围设计了地柜，用作起居被褥等物件的收纳。&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750278.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;img src="http://douxiubar.com/Upload/UploadFile/1461750306.jpeg"&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    ', '测试标题10', '2016-03-13 15:55:52.4018461', '2016-07-16 00:07:55.2175522', 0, '测试作者10', null, null, null, null, null, 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (15, 1, '3,4,11', 'test,test1,Test8', '
                        
                        
                        
                        
                        
                        
                        &lt;p&gt;测试标题11&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;
                    
                    
                    
                    
                    
                    
                    ', '测试标题11', '2016-07-15 23:51:24.9899144', '2016-10-23 17:25:51.5789861', 0, '漫漫洒洒', '/Upload/Cover/20161023/20161023163815.jpg', '测试标题11', '测试标题11', null, '测试标题11', 0, 1, null, null, 0);
INSERT INTO "main"."cms_article" VALUES (18, 4, null, null, '&lt;p&gt;测试标题2222&lt;span style="line-height: 1.8; word-spacing: normal;"&gt;测试标题2222&lt;/span&gt;&lt;span style="line-height: 1.8; word-spacing: normal;"&gt;测试标题2222&lt;/span&gt;&lt;span style="line-height: 1.8; word-spacing: normal;"&gt;测试标题2222&lt;/span&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题2222', '2016-10-23 17:28:27.670914', '2016-10-23 17:28:27.670914', 0, '漫漫洒洒', null, '测试标题2222测试标题2222测试标题2222测试标题2222', '测试标题2222', null, '测试标题2222测试标题2222测试标题2222测试标题2222', 0, 1, null, null, 0);

-- ----------------------------
-- Table structure for "main"."cms_articlemap"
-- ----------------------------
DROP TABLE "main"."cms_articlemap";
CREATE TABLE "cms_articlemap" (
"Ip"  TEXT NOT NULL,
"CreateTs"  INTEGER NOT NULL,
"ArticleId"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of cms_articlemap
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."cms_column"
-- ----------------------------
DROP TABLE "main"."cms_column";
CREATE TABLE "cms_column" (
"Column_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Sort"  INTEGER,
"ColumnName"  TEXT NOT NULL,
"Pid"  INTEGER NOT NULL,
"Lv"  INTEGER
);

-- ----------------------------
-- Records of cms_column
-- ----------------------------
INSERT INTO "main"."cms_column" VALUES (1, 1, '传闻爆料', 8, 2);
INSERT INTO "main"."cms_column" VALUES (2, 2, '科技财经', 0, 1);
INSERT INTO "main"."cms_column" VALUES (3, 3, '电子商务', 8, 2);
INSERT INTO "main"."cms_column" VALUES (4, 2, '人物动态', 8, 2);
INSERT INTO "main"."cms_column" VALUES (8, 1, '互联网媒体', 0, 1);
INSERT INTO "main"."cms_column" VALUES (13, 1, '汽车房产', 0, 1);
INSERT INTO "main"."cms_column" VALUES (15, 1, 'IT业界', 2, 2);
INSERT INTO "main"."cms_column" VALUES (16, 1, '经济民生', 2, 2);
INSERT INTO "main"."cms_column" VALUES (17, 1, '新车行情', 13, 2);
INSERT INTO "main"."cms_column" VALUES (18, 1, '房产动态', 13, 2);

-- ----------------------------
-- Table structure for "main"."cms_comment"
-- ----------------------------
DROP TABLE "main"."cms_comment";
CREATE TABLE "cms_comment" (
"Comment_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"ArticleId"  INTEGER NOT NULL,
"Content"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"Nickname"  TEXT NOT NULL,
"Email"  TEXT NOT NULL,
"VoteFavour"  INTEGER DEFAULT 0,
"VoteOppose"  INTEGER DEFAULT 0
);

-- ----------------------------
-- Records of cms_comment
-- ----------------------------
INSERT INTO "main"."cms_comment" VALUES (6, 11, 3333, '2016-07-25 23:52:36.349923', 2222, '123@qq.com', null, null);

-- ----------------------------
-- Table structure for "main"."cms_commentmap"
-- ----------------------------
DROP TABLE "main"."cms_commentmap";
CREATE TABLE "cms_commentmap" (
"Ip"  TEXT NOT NULL,
"CreateTs"  INTEGER NOT NULL,
"CommentId"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of cms_commentmap
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."cms_staticpage"
-- ----------------------------
DROP TABLE "main"."cms_staticpage";
CREATE TABLE "cms_staticpage" (
"Page_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"PagePath"  TEXT,
"PageName"  TEXT NOT NULL,
"TempPageId"  TEXT NOT NULL,
"PageParameter"  TEXT NOT NULL,
"CreateTime"  DATETIME NOT NULL,
"LastTime"  DATETIME NOT NULL
);

-- ----------------------------
-- Records of cms_staticpage
-- ----------------------------
INSERT INTO "main"."cms_staticpage" VALUES (2, '/html/pages/test.html', 'test', 3, '[{"key":"title","value":"标题","label":"标题","tiplabel":"标题","type":"1"},{"key":"content","value":"内容<p>1111</p>","label":"内容","type":"2"}]', '2016-05-29 23:31:43.661482', '2016-05-29 23:50:33.464836');
INSERT INTO "main"."cms_staticpage" VALUES (3, '/html/pages/test1.html', 'test1', 2, '[{"key":"title","value":"标题","label":"标题","tiplabel":"标题","type":"1"},{"key":"siteDescr","value":"描述","label":"描述","tiplabel":"描述","type":"1"},{"key":"siteKeywords","value":"关键字","label":"关键字","tiplabel":"关键字","type":"1"},{"key":"content","value":"内容<p><br></p>","label":"内容","type":"2"}]', '2016-07-30 22:35:16.3925746', '2016-07-30 22:35:16.3925746');

-- ----------------------------
-- Table structure for "main"."cms_tag"
-- ----------------------------
DROP TABLE "main"."cms_tag";
CREATE TABLE "cms_tag" (
"Tag_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"TagName"  TEXT NOT NULL,
"TagDes"  TEXT,
"LastTime"  DATETIME,
"CreateTime"  DATETIME NOT NULL
);

-- ----------------------------
-- Records of cms_tag
-- ----------------------------
INSERT INTO "main"."cms_tag" VALUES (1, 'Test', null, '2016-07-09 22:30:21.2123563', '2016-07-09 22:20:46.3794778');
INSERT INTO "main"."cms_tag" VALUES (3, 'test', null, '2016-07-13 00:16:12.0390339', '2016-07-13 00:16:12.0390339');
INSERT INTO "main"."cms_tag" VALUES (4, 'test1', null, '2016-07-13 00:20:11.600736', '2016-07-13 00:20:11.600736');
INSERT INTO "main"."cms_tag" VALUES (5, 'test2', 'test1', '2016-07-13 00:20:19.730201', '2016-07-13 00:20:19.730201');
INSERT INTO "main"."cms_tag" VALUES (6, 'test3', 'test3', '2016-07-13 00:20:29.9697867', '2016-07-13 00:20:29.9697867');
INSERT INTO "main"."cms_tag" VALUES (7, 'test4', 'test4', '2016-07-13 00:20:38.1902569', '2016-07-13 00:20:38.1902569');
INSERT INTO "main"."cms_tag" VALUES (8, 'test5', 'test5', '2016-07-13 00:20:45.0026465', '2016-07-13 00:20:45.0026465');
INSERT INTO "main"."cms_tag" VALUES (9, 'test6', 'test6', '2016-07-13 00:20:53.5931379', '2016-07-13 00:20:53.5931379');
INSERT INTO "main"."cms_tag" VALUES (10, 'test7', 'test7', '2016-07-13 00:21:09.9550737', '2016-07-13 00:21:09.9550737');
INSERT INTO "main"."cms_tag" VALUES (11, 'Test8', 'Test8', '2016-07-13 00:21:17.1684863', '2016-07-13 00:21:17.1684863');
INSERT INTO "main"."cms_tag" VALUES (12, 'Test9', 'Test9', '2016-07-13 00:21:25.0219355', '2016-07-13 00:21:25.0219355');
INSERT INTO "main"."cms_tag" VALUES (13, '赵玉开5', null, '2016-07-15 15:20:49', '2016-07-15 15:20:49');
INSERT INTO "main"."cms_tag" VALUES (14, '赵玉开5', null, '2016-07-15 15:21:21', '2016-07-15 15:21:21');

-- ----------------------------
-- Table structure for "main"."cms_tagmap"
-- ----------------------------
DROP TABLE "main"."cms_tagmap";
CREATE TABLE "cms_tagmap" (
"m_TagId"  INTEGER NOT NULL,
"m_ArticleId"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of cms_tagmap
-- ----------------------------
INSERT INTO "main"."cms_tagmap" VALUES (11, 3);
INSERT INTO "main"."cms_tagmap" VALUES (11, 4);
INSERT INTO "main"."cms_tagmap" VALUES (11, 9);
INSERT INTO "main"."cms_tagmap" VALUES (1, 11);
INSERT INTO "main"."cms_tagmap" VALUES (3, 11);
INSERT INTO "main"."cms_tagmap" VALUES (9, 11);

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
INSERT INTO "main"."cms_temppage" VALUES (2, 'Default', '默认模板', '[{ "key": "title", "value": "标题", "label": "标题", "tiplabel": "标题", "type": "1" },{ "key": "siteDescr", "value": "描述", "label": "描述", "tiplabel": "描述", "type": "1" },{ "key": "siteKeywords", "value": "关键字", "label": "关键字", "tiplabel": "关键字", "type": "1" }, { "key": "content", "value": "内容", "label": "内容", "type": "2" }]', '/Template/Default.cshtml', '@{ 
    Layout = "~/Views/Shared/_BlogLayout.cshtml";
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
                发表于 @(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                

            </p>
        </header>

        <div class="post-description-detail">
            @Html.Raw(obj["content"].ToString())
        </div>
    </section>
</div>', '2016-05-29 17:53:42.9683307', '2016-11-27 20:25:25.4551776');
INSERT INTO "main"."cms_temppage" VALUES (3, 'Default2', '默认模板2', '[{ "key": "title", "value": "标题", "label": "标题", "tiplabel": "标题", "type": "1" }, { "key": "content", "value": "内容", "label": "内容", "type": "2" }]', '/Template/Default2.cshtml', '@{
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
                发表于 @(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            </p>
        </header>

        <div class="post-description-detail">
            @Html.Raw(obj["content"].ToString())
        </div>
    </section>
</div>', '2016-05-29 21:36:42.6445554', '2016-11-27 20:25:11.039353');

-- ----------------------------
-- Table structure for "main"."sqlite_sequence"
-- ----------------------------
DROP TABLE "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_role', 5);
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_user', 3190);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_temppage', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_tag', 14);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_staticpage', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_staff', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('store_customer', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_column', 35);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_comment', 6);
INSERT INTO "main"."sqlite_sequence" VALUES ('cms_article', 18);
INSERT INTO "main"."sqlite_sequence" VALUES ('sys_log', 0);

-- ----------------------------
-- Table structure for "main"."store_category"
-- ----------------------------
DROP TABLE "main"."store_category";
CREATE TABLE "store_category" (
"Cat_Id"  INTEGER NOT NULL,
"StoreId"  INTEGER NOT NULL,
"Cat_Name"  TEXT NOT NULL,
"Cat_Des"  TEXT,
"PId"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
"LastTime"  DateTime NOT NULL,
PRIMARY KEY ("Cat_Id" ASC)
);

-- ----------------------------
-- Records of store_category
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_commodity"
-- ----------------------------
DROP TABLE "main"."store_commodity";
CREATE TABLE "store_commodity" (
"C_Id"  INTEGER NOT NULL,
"StoreId"  INTEGER NOT NULL,
"C_Name"  TEXT NOT NULL,
"C_Price"  INTEGER NOT NULL,
"C_PicPath"  TEXT NOT NULL,
"C_Stock"  INTEGER NOT NULL,
"C_ProName"  TEXT,
"C_ProDetail"  TEXT,
"C_Des"  TEXT NOT NULL,
"CatId"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
"LastTime"  DateTime NOT NULL,
PRIMARY KEY ("C_Id" ASC)
);

-- ----------------------------
-- Records of store_commodity
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_customer"
-- ----------------------------
DROP TABLE "main"."store_customer";
CREATE TABLE "store_customer" (
"U_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"UserName"  TEXT NOT NULL,
"UserPwd"  TEXT NOT NULL,
"NickName"  TEXT NOT NULL,
"Phone"  TEXT NOT NULL,
"Sex"  INTEGER NOT NULL,
"AvatarPath"  TEXT NOT NULL,
"IsUsable"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
"LastTime"  DateTime NOT NULL,
"TargetIp"  TEXT NOT NULL
);

-- ----------------------------
-- Records of store_customer
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_info"
-- ----------------------------
DROP TABLE "main"."store_info";
CREATE TABLE "store_info" (
"Store_Id"  INTEGER NOT NULL,
"StoreName"  TEXT NOT NULL,
"StoreLogoPath"  TEXT NOT NULL,
"StoreDes"  TEXT NOT NULL,
"StoreSwitch"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
"LastTime"  DateTime NOT NULL,
PRIMARY KEY ("Store_Id" ASC)
);

-- ----------------------------
-- Records of store_info
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_order"
-- ----------------------------
DROP TABLE "main"."store_order";
CREATE TABLE "store_order" (
"Order_Id"  INTEGER NOT NULL,
"StoreId"  INTEGER NOT NULL,
"UId"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
"Des"  TEXT NOT NULL,
"State"  INTEGER NOT NULL,
PRIMARY KEY ("Order_Id" ASC)
);

-- ----------------------------
-- Records of store_order
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_orderDetail"
-- ----------------------------
DROP TABLE "main"."store_orderDetail";
CREATE TABLE "store_orderDetail" (
"Od_Id"  INTEGER NOT NULL,
"OId"  INTEGER NOT NULL,
"Name"  TEXT NOT NULL,
"Num"  INTEGER NOT NULL,
"Price"  INTEGER NOT NULL,
"CreateTime"  DateTime NOT NULL,
PRIMARY KEY ("Od_Id" ASC)
);

-- ----------------------------
-- Records of store_orderDetail
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."store_staff"
-- ----------------------------
DROP TABLE "main"."store_staff";
CREATE TABLE "store_staff" (
"Staff_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"StoreId"  INTEGER NOT NULL,
"UserName"  TEXT NOT NULL,
"UserPwd"  TEXT NOT NULL,
"NickName"  TEXT NOT NULL,
"Phone"  TEXT NOT NULL,
"Sex"  INTEGER NOT NULL,
"AvatarPath"  TEXT NOT NULL,
"IsUsable"  INTEGER NOT NULL,
"DateTime"  DateTime NOT NULL,
"CreateTime"  DateTime NOT NULL,
"TargetIp"  TEXT NOT NULL
);

-- ----------------------------
-- Records of store_staff
-- ----------------------------

-- ----------------------------
-- Table structure for "main"."sys_log"
-- ----------------------------
DROP TABLE "main"."sys_log";
CREATE TABLE "sys_log" (
"Log_Id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"UserId"  INTEGER NOT NULL,
"LogInfo"  TEXT NOT NULL,
"CreateTs"  INTEGER NOT NULL
);

-- ----------------------------
-- Records of sys_log
-- ----------------------------

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
INSERT INTO "main"."sys_role" VALUES (1, '超级管理员', null);
INSERT INTO "main"."sys_role" VALUES (3, '测试管理员', 'Blog|blogManage|Show,Blog|blogManage|Edit,Blog|blogManage|Delete,Blog|blogManage|Upload,Blog|columnManage|Show,Blog|columnManage|Add,Blog|columnManage|Edit,Blog|columnManage|Delete,Blog|fileManage|Show,Blog|fileManage|Delete,Blog|fileManage|Edit,Blog|fileManage|Add,Blog|fileManage|Upload,Blog|staticHtmlManage|Show,Blog|staticHtmlManage|Add,Blog|staticHtmlManage|Edit,Blog|staticHtmlManage|Delete,Blog|staticHtmlManage|Build,Blog|templateManage|Show,Blog|templateManage|Add,Blog|templateManage|Edit,Blog|templateManage|Delete,Blog|templateManage|Build,Blog|tagManage|Show,Blog|tagManage|Edit,Blog|tagManage|Add,Blog|commentManage|Show,Blog|commentManage|Delete,Blog|UIManage|Show,Blog|UIManage|Edit,Blog|UIManage|Select,Blog|configManage|Show,Blog|configManage|Edit,Root|myCenter|Show,Root|myCenter|Edit');

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
INSERT INTO "main"."sys_user" VALUES (1, 1, '702295399@qq.com', 'cebfd1559b68d67688884d7a3d3e8c', 'fourone', '/Upload/Avatar/Avatar_1.jpg', '702295399@qq.com', 1888888889, 702295399, '2016-02-07 09:44:53.2348994', '2016-11-27 19:03:34.1222654', '127.0.0.1', 1);
INSERT INTO "main"."sys_user" VALUES (3190, 3, '测试账户', 'cebfd1559b68d67688884d7a3d3e8c', '测试小二', '/Upload/Avatar/Avatar_3190.jpg', '7022953991@qq.com', 1234567891, 1234567891, '2016-02-19 10:26:13.8261764', '2016-10-13 09:04:00.5456769', '127.0.0.1', 0);

-- ----------------------------
-- Indexes structure for table cms_articlemap
-- ----------------------------
CREATE INDEX "main"."sel_index_a"
ON "cms_articlemap" ("Ip" ASC, "ArticleId" ASC, "CreateTs" ASC);

-- ----------------------------
-- Indexes structure for table cms_commentmap
-- ----------------------------
CREATE INDEX "main"."sel_index"
ON "cms_commentmap" ("Ip" ASC, "CommentId" ASC, "CreateTs" ASC);
