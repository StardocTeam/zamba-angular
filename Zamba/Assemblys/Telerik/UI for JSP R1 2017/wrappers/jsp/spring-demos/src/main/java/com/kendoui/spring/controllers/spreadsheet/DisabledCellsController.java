package com.kendoui.spring.controllers.spreadsheet;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;


@Controller("spreadsheet-disabledcells-controller")
@RequestMapping(value="/spreadsheet/")
public class DisabledCellsController {
    @RequestMapping(value = {"/disabled-cells"}, method = RequestMethod.GET)
    public String index() {
        return "spreadsheet/disabled-cells";
    }
}

