package com.kendoui.spring.controllers.editor;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("editor-paste-cleanup-controller")
@RequestMapping(value="/editor/")
public class PasteCleanupController {
    @RequestMapping(value = "/paste-cleanup", method = RequestMethod.GET)
    public String index() {       
        return "editor/paste-cleanup";
    }    
}