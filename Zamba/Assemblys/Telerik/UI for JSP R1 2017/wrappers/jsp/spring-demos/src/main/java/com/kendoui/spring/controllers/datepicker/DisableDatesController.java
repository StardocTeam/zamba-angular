package com.kendoui.spring.controllers.datepicker;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("datepicker-disabled-controller")
@RequestMapping(value="/datepicker/")
public class DisableDatesController {
    
    @RequestMapping(value = {"/disabled-dates"}, method = RequestMethod.GET)
    public String index() {       
        return "datepicker/disabled-dates";
    }    
}