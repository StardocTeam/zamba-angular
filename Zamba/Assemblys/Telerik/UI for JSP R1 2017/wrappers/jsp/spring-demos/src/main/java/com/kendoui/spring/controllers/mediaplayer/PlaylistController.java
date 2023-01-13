package com.kendoui.spring.controllers.mediaplayer;

import java.util.ArrayList;
import java.util.Locale;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import com.kendoui.spring.models.Video;

@Controller("mediaplayer-playlist-controller")
@RequestMapping(value = "/mediaplayer/")
public class PlaylistController {

    @RequestMapping(value = "playlist", method = RequestMethod.GET)
    public String index(Locale locale, Model model) {

        ArrayList<Video> videos = new ArrayList<Video>();
        videos.add(new Video(
                "Telerik Platform - Enterprise Mobility. Unshackled.",
                "https://www.youtube.com/watch?v=N3P6MyvL-t4",
                "http://img.youtube.com/vi/N3P6MyvL-t4/1.jpg"));
        videos.add(new Video("Learn How York Solved Its Database Problem",
                "https://www.youtube.com/watch?v=_S63eCewxRg",
                "http://img.youtube.com/vi/_S63eCewxRg/1.jpg"));
        videos.add(new Video(
                "Responsive Website Delivers for Reeves Import Motorcars",
                "https://www.youtube.com/watch?v=DYsiJRmIQZw",
                "http://img.youtube.com/vi/DYsiJRmIQZw/1.jpg"));
        videos.add(new Video("Digital Transformation: A New Way of Thinking",
                "https://www.youtube.com/watch?v=gNlya720gbk",
                "http://img.youtube.com/vi/gNlya720gbk/1.jpg"));
        videos.add(new Video("Take a Tour of the Telerik Platform",
                "https://www.youtube.com/watch?v=rLtTuFbuf1c",
                "http://img.youtube.com/vi/rLtTuFbuf1c/1.jpg"));
        videos.add(new Video(
                "Why Telerik Analytics - Key Benefits for your applications",
                "https://www.youtube.com/watch?v=CpHKm2NruYc",
                "https://i.ytimg.com/vi/CpHKm2NruYc/1.jpg"));

        model.addAttribute("videos", videos);

        return "mediaplayer/playlist";
    }
}
