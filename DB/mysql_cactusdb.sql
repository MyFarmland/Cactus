/*
Navicat MySQL Data Transfer

Source Server         : cactusdb
Source Server Version : 50077
Source Host           : localhost:3306
Source Database       : cactusdb

Target Server Type    : MYSQL
Target Server Version : 50077
File Encoding         : 65001

Date: 2016-06-11 21:46:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `cms_article`
-- ----------------------------
DROP TABLE IF EXISTS `cms_article`;
CREATE TABLE `cms_article` (
  `article_id` bigint(20) NOT NULL auto_increment,
  `columnid` bigint(20) default NULL,
  `tags` varchar(255) default NULL,
  `articlecontent` longtext,
  `title` varchar(255) default NULL,
  `createtime` datetime default NULL,
  `lasttime` datetime default NULL,
  `browse` bigint(20) default NULL,
  `author` varchar(255) default NULL,
  `istop` int(1) default NULL,
  `isshow` int(1) default NULL,
  PRIMARY KEY  (`article_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cms_article
-- ----------------------------
INSERT INTO `cms_article` VALUES ('1', '2', null, '&lt;p&gt;ArticleContent&lt;/p&gt;&lt;p&gt;11&lt;/p&gt;', '测试标题', '1899-12-30 00:00:00', '2016-06-09 19:28:47', '0', '测试作者', '0', '1');
INSERT INTO `cms_article` VALUES ('2', '4', '', '&lt;p&gt;&lt;b&gt;求大神&lt;/b&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题1111', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者2222', '0', '1');
INSERT INTO `cms_article` VALUES ('3', '1', '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题2', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者2', '0', '1');
INSERT INTO `cms_article` VALUES ('4', '2', '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题3', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者3', '0', '1');
INSERT INTO `cms_article` VALUES ('5', '1', '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题4', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者4', '0', '1');
INSERT INTO `cms_article` VALUES ('6', '1', '', '&lt;p&gt;&lt;u&gt;&lt;b&gt;测试内容&lt;/b&gt;&lt;/u&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题5', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者5', '0', '1');
INSERT INTO `cms_article` VALUES ('7', '2', '', '&lt;p&gt;&lt;i&gt;测试内容&lt;/i&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题6', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者6', '0', '1');
INSERT INTO `cms_article` VALUES ('8', '1', '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题7', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者7', '1', '0');
INSERT INTO `cms_article` VALUES ('9', '2', '', '\r\n                        &lt;p&gt;测试内容&lt;img src=\"/Upload/UploadFile/1457864405.jpeg\" style=\"letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;\"&gt;&lt;/p&gt;\r\n                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题8', '0001-01-01 00:00:00', '1899-12-30 00:00:00', '0', '测试作者8', '0', '0');
INSERT INTO `cms_article` VALUES ('10', '3', '', '\r\n                        &lt;p&gt;测试内容&lt;img src=\"/Upload/UploadFile/1457864442.jpeg\" style=\"letter-spacing: 0.01em; word-spacing: normal; max-width: 100%;\"&gt;&lt;/p&gt;\r\n                    &lt;p&gt;&lt;br&gt;&lt;/p&gt;', '测试标题9', '0001-01-01 00:00:00', '1899-12-30 00:00:00', '0', '测试作者9', '1', '1');
INSERT INTO `cms_article` VALUES ('11', '1', '', '&lt;p&gt;测试内容&lt;/p&gt;', '测试标题10', '1899-12-30 00:00:00', '1899-12-30 00:00:00', '0', '测试作者10', '0', '1');

-- ----------------------------
-- Table structure for `cms_column`
-- ----------------------------
DROP TABLE IF EXISTS `cms_column`;
CREATE TABLE `cms_column` (
  `column_id` bigint(20) NOT NULL auto_increment,
  `sort` bigint(20) default NULL,
  `columnname` varchar(255) default NULL,
  PRIMARY KEY  (`column_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cms_column
-- ----------------------------
INSERT INTO `cms_column` VALUES ('1', '1', '测试1');
INSERT INTO `cms_column` VALUES ('2', '1', '测试2');
INSERT INTO `cms_column` VALUES ('3', '1', '测试3');
INSERT INTO `cms_column` VALUES ('4', '1', '测试4');
INSERT INTO `cms_column` VALUES ('5', '1', '测试1');
INSERT INTO `cms_column` VALUES ('7', '2', '测试6');
INSERT INTO `cms_column` VALUES ('8', '1', '我的栏目');
INSERT INTO `cms_column` VALUES ('9', '10', '我的栏目');
INSERT INTO `cms_column` VALUES ('10', '1', '测试栏目11111');

-- ----------------------------
-- Table structure for `cms_staticpage`
-- ----------------------------
DROP TABLE IF EXISTS `cms_staticpage`;
CREATE TABLE `cms_staticpage` (
  `Page_Id` int(11) NOT NULL auto_increment,
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gb2312;

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
  `role_id` bigint(20) NOT NULL auto_increment,
  `rolename` varchar(255) default NULL,
  `actionids` varchar(255) default NULL,
  PRIMARY KEY  (`role_id`)
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
  `user_id` bigint(20) NOT NULL,
  `roleid` varchar(255) default NULL,
  `name` varchar(255) default NULL,
  `password` varchar(255) default NULL,
  `nickname` varchar(255) default NULL,
  `avatar` varchar(255) default NULL,
  `email` varchar(255) default NULL,
  `phone` varchar(255) default NULL,
  `qq` varchar(255) default NULL,
  `addtime` datetime default NULL,
  `lastlogintime` datetime default NULL,
  `lastloginip` varchar(255) default NULL,
  `issuperuser` int(1) default NULL,
  PRIMARY KEY  (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES ('1', '1', '702295399@qq.com', 'cebfd1559b68d67688884d7a3d3e8c', '漫漫洒洒', '/Upload/Avatar/Avatar_1.jpg', '702295399@qq.com', '138888888888', '702295399', '1899-12-30 00:00:00', '2016-06-09 19:21:50', '127.0.0.1', '1');
INSERT INTO `sys_user` VALUES ('3190', '3', '测试三', 'cebfd1559b68d67688884d7a3d3e8c', '测试三1', '/Upload/Avatar/Avatar_3190.jpg', '7022953991@qq.com', '1234567891', '1234567891', '1899-12-30 00:00:00', '2016-06-09 19:20:28', '127.0.0.1', '0');
