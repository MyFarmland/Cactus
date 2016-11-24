/*
Navicat MySQL Data Transfer

Source Server         : cactusdb
Source Server Version : 50077
Source Host           : localhost:3306
Source Database       : cactusdb

Target Server Type    : MYSQL
Target Server Version : 50077
File Encoding         : 65001

Date: 2016-11-23 10:58:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `cms_article`
-- ----------------------------
DROP TABLE IF EXISTS `cms_article`;
CREATE TABLE `cms_article` (
  `Article_Id` bigint(20) NOT NULL auto_increment,
  `ColumnId` bigint(20) NOT NULL,
  `Tagids` varchar(255) default NULL,
  `Tags` varchar(255) default NULL,
  `ArticleContent` longtext NOT NULL,
  `Title` varchar(255) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `LastTime` datetime NOT NULL,
  `Browse` bigint(20) NOT NULL default '0',
  `Author` varchar(255) default NULL,
  `ImgUrl` varchar(255) default NULL,
  `Digest` varchar(255) default NULL,
  `SEO_Title` varchar(255) default NULL,
  `SEO_Keywords` varchar(255) default NULL,
  `SEO_DES` varchar(255) default NULL,
  `IsTop` int(1) NOT NULL,
  `IsShow` int(1) NOT NULL,
  `Source` varchar(100) default NULL,
  `SourceLink` varchar(200) default NULL,
  `Like` int(11) default NULL,
  PRIMARY KEY  (`Article_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cms_article
-- ----------------------------
INSERT INTO `cms_article` VALUES ('1', '2', null, null, '&lt;p&gt;ArticleContent&lt;/p&gt;&lt;p&gt;11&lt;/p&gt;', '测试标题', '1899-12-30 00:00:00', '2016-06-09 19:28:47', '0', '测试作者', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('2', '4', null, '', '&lt;p&gt;&lt;b&gt;求大神&lt;/b&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题1111', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者2222', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('3', '1', null, '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题2', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者2', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('4', '2', null, '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题3', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者3', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('5', '1', null, '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题4', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者4', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('6', '1', null, '', '&lt;p&gt;&lt;u&gt;&lt;b&gt;测试内容&lt;/b&gt;&lt;/u&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题5', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者5', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('7', '2', null, '', '&lt;p&gt;&lt;i&gt;测试内容&lt;/i&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题6', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者6', null, null, null, null, null, '0', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('8', '1', null, '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题7', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者7', null, null, null, null, null, '1', '0', null, null, null);
INSERT INTO `cms_article` VALUES ('9', '2', null, '', '\r\n                        &lt;p&gt;测试内容&lt;img src=\"/Upload/UploadFile/1457864405.jpeg\" style=\"letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;\"&gt;&lt;/p&gt;\r\n                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题8', '0001-01-01 00:00:00', '1899-12-30 00:00:00', '0', '测试作者8', null, null, null, null, null, '0', '0', null, null, null);
INSERT INTO `cms_article` VALUES ('10', '3', null, '', '\r\n                        &lt;p&gt;测试内容&lt;img src=\"/Upload/UploadFile/1457864442.jpeg\" style=\"letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;\"&gt;&lt;/p&gt;\r\n                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题9', '0001-01-01 00:00:00', '1899-12-30 00:00:00', '0', '测试作者9', null, null, null, null, null, '1', '1', null, null, null);
INSERT INTO `cms_article` VALUES ('11', '1', null, '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题10', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者10', null, null, null, null, null, '0', '1', null, null, null);

-- ----------------------------
-- Table structure for `cms_articlemap`
-- ----------------------------
DROP TABLE IF EXISTS `cms_articlemap`;
CREATE TABLE `cms_articlemap` (
  `Ip` varchar(255) NOT NULL,
  `CreateTs` bigint(20) NOT NULL,
  `ArticleId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cms_articlemap
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_column`
-- ----------------------------
DROP TABLE IF EXISTS `cms_column`;
CREATE TABLE `cms_column` (
  `column_id` bigint(20) NOT NULL auto_increment,
  `sort` bigint(20) default NULL,
  `columnname` varchar(255) NOT NULL,
  `Pid` bigint(20) NOT NULL,
  `Lv` int(11) default NULL,
  PRIMARY KEY  (`column_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cms_column
-- ----------------------------
INSERT INTO `cms_column` VALUES ('1', '1', '测试1', '0', null);
INSERT INTO `cms_column` VALUES ('2', '1', '测试2', '0', null);
INSERT INTO `cms_column` VALUES ('3', '1', '测试3', '0', null);
INSERT INTO `cms_column` VALUES ('4', '1', '测试4', '0', null);
INSERT INTO `cms_column` VALUES ('5', '1', '测试1', '0', null);
INSERT INTO `cms_column` VALUES ('7', '2', '测试6', '0', null);
INSERT INTO `cms_column` VALUES ('8', '1', '我的栏目', '0', null);
INSERT INTO `cms_column` VALUES ('9', '10', '我的栏目', '0', null);
INSERT INTO `cms_column` VALUES ('10', '1', '测试栏目11111', '0', null);

-- ----------------------------
-- Table structure for `cms_comment`
-- ----------------------------
DROP TABLE IF EXISTS `cms_comment`;
CREATE TABLE `cms_comment` (
  `Comment_Id` bigint(20) NOT NULL,
  `ArticleId` bigint(20) NOT NULL,
  `Content` varchar(255) NOT NULL,
  `CreateTime` datetime NOT NULL,
  `Nickname` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `VoteFavour` int(11) default '0',
  `VoteOppose` int(11) default '0',
  PRIMARY KEY  (`Comment_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cms_comment
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_commentmap`
-- ----------------------------
DROP TABLE IF EXISTS `cms_commentmap`;
CREATE TABLE `cms_commentmap` (
  `Ip` varchar(20) NOT NULL,
  `CreateTs` bigint(20) NOT NULL,
  `CommentId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cms_commentmap
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_staticpage`
-- ----------------------------
DROP TABLE IF EXISTS `cms_staticpage`;
CREATE TABLE `cms_staticpage` (
  `Page_Id` int(11) NOT NULL auto_increment,
  `PagePath` varchar(255) default NULL,
  `PageName` varchar(10) default NULL,
  `TempPageId` int(11) default NULL,
  `PageParameter` varchar(20) default NULL,
  `CreateTime` datetime default NULL,
  `LastTime` datetime default NULL,
  PRIMARY KEY  (`Page_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Records of cms_staticpage
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_tag`
-- ----------------------------
DROP TABLE IF EXISTS `cms_tag`;
CREATE TABLE `cms_tag` (
  `Tag_Id` bigint(20) NOT NULL,
  `TagName` varchar(100) NOT NULL,
  `TagDes` varchar(255) default NULL,
  `LastTime` datetime default NULL,
  `CreateTime` datetime NOT NULL,
  PRIMARY KEY  (`Tag_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cms_tag
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_tagmap`
-- ----------------------------
DROP TABLE IF EXISTS `cms_tagmap`;
CREATE TABLE `cms_tagmap` (
  `m_TagId` bigint(20) NOT NULL,
  `m_ArticleId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of cms_tagmap
-- ----------------------------

-- ----------------------------
-- Table structure for `cms_temppage`
-- ----------------------------
DROP TABLE IF EXISTS `cms_temppage`;
CREATE TABLE `cms_temppage` (
  `TempPage_Id` int(11) NOT NULL auto_increment,
  `TempName` varchar(10) default NULL,
  `TempByname` varchar(20) default NULL,
  `TempParameter` varchar(10) default NULL,
  `TempPath` varchar(20) default NULL,
  `TempContent` varchar(200) default NULL,
  `CreateTime` datetime default NULL,
  `LastTime` datetime default NULL,
  PRIMARY KEY  (`TempPage_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Records of cms_temppage
-- ----------------------------

-- ----------------------------
-- Table structure for `store_category`
-- ----------------------------
DROP TABLE IF EXISTS `store_category`;
CREATE TABLE `store_category` (
  `category_id` bigint(20) NOT NULL auto_increment,
  `categoryname` varchar(255) default NULL,
  `groupcode` bigint(20) default NULL,
  PRIMARY KEY  (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of store_category
-- ----------------------------

-- ----------------------------
-- Table structure for `store_item`
-- ----------------------------
DROP TABLE IF EXISTS `store_item`;
CREATE TABLE `store_item` (
  `item_id` bigint(20) NOT NULL auto_increment,
  `title` varchar(255) default NULL,
  `categoryid` bigint(20) default NULL,
  `content` longtext,
  `price` bigint(20) default NULL,
  `stock` bigint(20) default NULL,
  `addtime` datetime default NULL,
  `state` int(1) default NULL,
  `flag` int(1) default NULL,
  PRIMARY KEY  (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of store_item
-- ----------------------------

-- ----------------------------
-- Table structure for `store_member`
-- ----------------------------
DROP TABLE IF EXISTS `store_member`;
CREATE TABLE `store_member` (
  `member_id` bigint(20) NOT NULL auto_increment,
  `loginname` varchar(255) default NULL,
  `password` varchar(255) default NULL,
  `addtime` datetime default NULL,
  `lasttime` datetime default NULL,
  `avatarpath` varchar(255) default NULL,
  `phone` varchar(255) default NULL,
  `state` int(1) default NULL,
  PRIMARY KEY  (`member_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of store_member
-- ----------------------------

-- ----------------------------
-- Table structure for `store_order`
-- ----------------------------
DROP TABLE IF EXISTS `store_order`;
CREATE TABLE `store_order` (
  `order_id` bigint(20) NOT NULL,
  `itemid` bigint(20) default NULL,
  `price` bigint(20) default NULL,
  `num` bigint(20) default NULL,
  `des` varchar(255) default NULL,
  `address` varchar(255) default NULL,
  `phone` varchar(255) default NULL,
  `state` bigint(20) default NULL,
  `addtime` datetime default NULL,
  `isdeliver` int(1) default NULL,
  `trackingnumber` varchar(255) default NULL,
  `delivertime` datetime default NULL,
  `isreceipt` int(1) default NULL,
  `receipttime` datetime default NULL,
  `comment` varchar(255) default NULL,
  `memberid` bigint(20) default NULL,
  PRIMARY KEY  (`order_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of store_order
-- ----------------------------

-- ----------------------------
-- Table structure for `sys_role`
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role` (
  `Role_id` bigint(20) NOT NULL auto_increment,
  `RoleName` varchar(255) default NULL,
  `ActionIds` varchar(255) default NULL,
  PRIMARY KEY  (`Role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES ('1', 'RoleTest2222', '1008,1009,1010,1011,1001,1002,1003,1004,1005,1006,1007,1012,1013,1014,1015,1016,1017,1018,1019,1020,1021,1022,2001,2002');
INSERT INTO `sys_role` VALUES ('3', '测试', '1009,1010,1011,1001,1004,1005,1006,1007');

-- ----------------------------
-- Table structure for `sys_user`
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user` (
  `User_Id` bigint(20) NOT NULL,
  `Roleid` varchar(255) default NULL,
  `Name` varchar(255) default NULL,
  `Password` varchar(255) default NULL,
  `NickName` varchar(255) default NULL,
  `Avatar` varchar(255) default NULL,
  `Email` varchar(255) default NULL,
  `Phone` varchar(255) default NULL,
  `Qq` varchar(255) default NULL,
  `Addtime` datetime default NULL,
  `LastLoginTime` datetime default NULL,
  `LastLoginIp` varchar(255) default NULL,
  `IsSuperUser` int(1) default NULL,
  PRIMARY KEY  (`User_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1', '1', '702295399@qq.com', 'cebfd1559b68d67688884d7a3d3e8c', '漫漫洒洒', '/Upload/Avatar/Avatar_1.jpg', '702295399@qq.com', '138888888888', '702295399', '1899-12-30 00:00:00', '2016-06-09 19:21:50', '127.0.0.1', '1');
INSERT INTO `sys_user` VALUES ('3190', '3', '测试三', 'cebfd1559b68d67688884d7a3d3e8c', '测试三1', '/Upload/Avatar/Avatar_3190.jpg', '7022953991@qq.com', '1234567891', '1234567891', '1899-12-30 00:00:00', '2016-06-09 19:20:28', '127.0.0.1', '0');
