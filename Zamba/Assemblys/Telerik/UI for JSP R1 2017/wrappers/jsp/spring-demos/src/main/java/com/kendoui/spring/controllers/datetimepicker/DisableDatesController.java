package com.kendoui.spring.controllers.datetimepicker;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("datetimepicker-disabled-controller")
@RequestMapping(value="/datetimepicker/")
public class DisableDatesController {
    
    @RequestMapping(value = {"/disable-dates"}, method = RequestMethod.GET)
    public String index() {       
        return "datetimepicker/disable-dates";
    }    
}