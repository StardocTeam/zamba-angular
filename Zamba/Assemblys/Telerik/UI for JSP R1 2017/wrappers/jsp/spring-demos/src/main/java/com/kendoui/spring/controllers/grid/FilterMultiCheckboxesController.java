package com.kendoui.spring.controllers.grid;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.kendoui.spring.models.Employee;
import com.kendoui.spring.models.EmployeeDao;

@Controller("filter-multi-checkboxes-controller")
@RequestMapping(value="/grid/")
public class FilterMultiCheckboxesController {
    @Autowired
    private EmployeeDao employee;

    @RequestMapping(value = "/filter-multi-checkboxes", method = RequestMethod.GET)
    public String index() {
        return "grid/filter-multi-checkboxes";
    }

    @RequestMapping(value = "/filter-multi-checkboxes/unique", method = RequestMethod.POST)
    public @ResponseBody List<Employee> unique(@RequestBody Map<String, Object> model) {
        String field = (String)model.get("field");
        List<Employee> allItems = employee.getList();

        List<Employee> result = new ArrayList<Employee>();
        HashSet<String> seen = new HashSet<String>();

        Class<?> cls = allItems.get(0).getClass();
        Field f;
        try {
          f = cls.getDeclaredField(field);
          f.setAccessible(true);
        } catch (NoSuchFieldException e) {
            return allItems;
        }

        for (Employee employee : allItems) {
            Object value;
            try {
                value = f.get(employee);
            } catch (IllegalAccessException e) {
                continue;
            }
            if(!seen.contains(value.toString())){
                seen.add(value.toString());
                result.add(employee);
            }
        }
        return result;
    }
}
