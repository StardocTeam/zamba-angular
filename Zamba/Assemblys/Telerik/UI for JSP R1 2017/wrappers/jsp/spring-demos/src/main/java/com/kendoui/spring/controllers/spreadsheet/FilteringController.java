package com.kendoui.spring.controllers.spreadsheet;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("spreadsheet-sorting-and-filtering-controller")
@RequestMapping(value="/spreadsheet/")
public class FilteringController {
    @RequestMapping(value = {"/sorting-filtering"}, method = RequestMethod.GET)
    public String index() {
        return "spreadsheet/sorting-filtering";
    }
}

