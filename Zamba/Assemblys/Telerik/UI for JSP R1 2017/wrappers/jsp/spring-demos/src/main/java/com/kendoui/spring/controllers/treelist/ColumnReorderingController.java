package com.kendoui.spring.controllers.treelist;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.kendoui.spring.models.DetailedEmployeeDirectory;
import com.kendoui.spring.models.EmployeeDirectoryDao;


@Controller("treelist-column-reordering-controller")
@RequestMapping(value="/treelist/")
public class ColumnReorderingController {
    
    @Autowired 
    private EmployeeDirectoryDao employeeDirectory;
    
    @RequestMapping(value = "/column-reordering", method = RequestMethod.GET)
    public String index() {
        return "treelist/column-reordering";
    }
        
    @RequestMapping(value = "/column-reordering/read", method = RequestMethod.POST)
    public @ResponseBody List<DetailedEmployeeDirectory> read(@RequestBody Map<String, Object> model) {
        return employeeDirectory.getByEmployeeId((Integer)model.get("id"));
    }   
}