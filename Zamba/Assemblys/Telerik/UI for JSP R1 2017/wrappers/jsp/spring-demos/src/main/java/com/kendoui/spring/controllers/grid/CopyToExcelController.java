package com.kendoui.spring.controllers.grid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.kendoui.spring.models.DataSourceRequest;
import com.kendoui.spring.models.DataSourceResult;
import com.kendoui.spring.models.OrderDao;

@Controller("copy-to-excel-controller")
@RequestMapping(value="/grid/")
public class CopyToExcelController {
    @Autowired 
    private OrderDao order;

    @RequestMapping(value = "/copy-to-excel", method = RequestMethod.GET)
    public String index() {
        return "grid/copy-to-excel";
    }
    
    @RequestMapping(value = "/copy-to-excel/read", method = RequestMethod.POST)
    public @ResponseBody DataSourceResult read(@RequestBody DataSourceRequest request) {

        return order.getList(request);
    }
}