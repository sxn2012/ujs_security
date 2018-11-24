#!/usr/bin/env python
# -*- encoding: utf-8 -*-
# Created on 2017-02-09 17:41:37
# Project: test2

from pyspider.libs.base_handler import *
import re
import requests
import xlwt
from datetime import datetime
import xlrd
import xlutils
from xlutils.copy import copy
import xlrd as ExcelRead 
import os


class Handler(BaseHandler):
    crawl_config = {
    }
       
    @every(minutes=24 * 60)
    def on_start(self):
        self.crawl('http://www.ujs.edu.cn/site1/node3', callback=self.index_page)

    @config(age=10 * 24 * 60 * 60)
    def index_page(self, response):      
        for each in response.doc('a[href^="http"][class="xw_list"]').items():
            self.crawl(each.attr.href, callback=self.detail_page)
                   
            
            
    @config(priority=2)
    def detail_page(self, response):
        response_=requests.get(response.doc('script[src^="http://www.ujs.edu.cn/counter.php"]').attr.src)
        response_result=response_.content.decode('utf-8')
        pattern=re.compile(r'\d+')
        responseContent=pattern.findall(response_result)
        title_origin=response.doc('p[class="neieyetitle"]').text().strip()
        time_origin=response.doc('span[class="fbtime"]').text().strip()

        
        file_name = 'G:/example.xls'
        r_xls = ExcelRead.open_workbook(file_name) 
        r_sheet = r_xls.sheet_by_index(0) 
        rows = r_sheet.nrows 
        w_xls = copy(r_xls) 
        sheet_write = w_xls.get_sheet(0) 
  

        sheet_write.write(rows,0,response.url)
        sheet_write.write(rows,1,title_origin)
        sheet_write.write(rows,2,time_origin[5:20])
        sheet_write.write(rows,3,responseContent[0])
        
        w_xls.save(file_name);
        
        
        return {
            "url": response.url,
            "title": title_origin,
            "time": time_origin[5:20],
            "amount": responseContent[0]
        }
    
