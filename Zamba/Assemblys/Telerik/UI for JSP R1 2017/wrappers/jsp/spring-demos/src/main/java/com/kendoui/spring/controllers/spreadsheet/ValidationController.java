package com.kendoui.spring.controllers.spreadsheet;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;


@Controller("spreadsheet-validation-controller")
@RequestMapping(value="/spreadsheet/")
public class ValidationController {
    @RequestMapping(value = {"/validation"}, method = RequestMethod.GET)
    public String index() {
        return "spreadsheet/validation";
    }
}

