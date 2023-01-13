package com.kendoui.spring.controllers.grid;

import java.util.Locale;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.kendoui.spring.models.EmployeeDao;

@Controller("grid-navigation-controller")
@RequestMapping(value="/grid/")
public class KeyboardNavigationController {
    @Autowired 
    private EmployeeDao employee;
    
    @RequestMapping(value = "/keyboard-navigation", method = RequestMethod.GET)
    public String index(Locale locale, Model model) {
        model.addAttribute("employees", employee.getList());
        
        return "grid/keyboard-navigation"; 
    }
}