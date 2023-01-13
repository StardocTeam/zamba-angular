package com.kendoui.spring.controllers.editor;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("editor-pdf-export-controller")
@RequestMapping(value="/editor/")
public class PdfExportController {
    
    @RequestMapping(value = "/pdf-export", method = RequestMethod.GET)
    public String index() {       
        return "editor/pdf-export";
    }    
}