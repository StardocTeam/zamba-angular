package com.kendoui.spring.controllers.editor;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("editor-immutable-elements-controller")
@RequestMapping(value="/editor/")
public class ImmutableElementsController {
    @RequestMapping(value = "/immutable-elements", method = RequestMethod.GET)
    public String index() {       
        return "editor/immutable-elements";
    }    
}