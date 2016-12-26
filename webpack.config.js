/*
* @Author: Marte
* @Date:   2016-12-07 14:01:14
* @Last Modified by:   Marte
* @Last Modified time: 2016-12-26 15:47:47
*/

var webpack = require('webpack');
var path = require('path');
var HtmlwebpackPlugin = require('html-webpack-plugin');
//定义了一些文件夹的路径
var ROOT_PATH = path.resolve(__dirname);
var APP_PATH = path.resolve(ROOT_PATH, 'ZDL.AnyCodes.Site/js');
var BUILD_PATH = path.resolve(ROOT_PATH, 'ZDL.AnyCodes.Site.Build');

module.exports = {
  //项目的文件夹 可以直接用文件夹名称 默认会找index.js 也可以确定是哪个文件名字
  entry: {
    bundle:APP_PATH,
    vendor:['jquery']
  },
  //输出的文件名 合并以后的js会命名为bundle.js
  output: {
    path: BUILD_PATH,
    filename: "[name].js"
  },
  devServer: {
    historyApiFallback: true,
    hot: true,
    inline: true,
    progress: true,
  },
  module: {
   loaders: [
     {
       test: /\.css$/,
       loaders: ['style', 'css'],
       include: APP_PATH
     },
     {
        test: /\.scss$/,
        loaders: ['style', 'css', 'sass'],
        include: APP_PATH
      }
   ]
 },
  //添加我们的插件 会自动生成一个html文件
  plugins: [
    new HtmlwebpackPlugin({
      title: 'Hello World app'
    }),
    new webpack.optimize.CommonsChunkPlugin('vendor',  'vendor.js')
  ]
};
