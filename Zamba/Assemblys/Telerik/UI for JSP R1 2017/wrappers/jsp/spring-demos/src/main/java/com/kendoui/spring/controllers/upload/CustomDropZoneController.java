package com.kendoui.spring.controllers.upload;

import java.util.List;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

@Controller("upload-custom-drop-zone-controller")
@RequestMapping(value="/upload/")
public class CustomDropZoneController {
    
    @RequestMapping(value = "/customdropzone", method = RequestMethod.GET)
    public String index() {
        return "upload/customdropzone";
    }
    
    @RequestMapping(value = "/customdropzone/save", method = RequestMethod.POST)
    public @ResponseBody String save(@RequestParam List<MultipartFile> files) {
        // Save the files
        // for (MultipartFile file : files) {
        // }
        
        // Return an empty string to signify success
        return "";
    }
    
    @RequestMapping(value = "/customdropzone/remove", method = RequestMethod.POST)
    public @ResponseBody String remove(@RequestParam String[] fileNames) {
        // Remove the files
        // for (String fileName : fileNames) {
        // }
        // Return an empty string to signify success
        return "";
    }
}