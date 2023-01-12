package com.kendoui.spring.models;


public class Video {
    private String title;
    private String source;
    private String poster;   
    
    public Video(String title, String source,String poster) {
        setTitle(title);
        setSource(source);
        setPoster(poster);
    }

    public String getTitle() {
        return title;
    }
    public String getSource() {
        return source;
    }
    public String getPoster() {
        return poster;
    }
    public void setTitle(String title) {
        this.title = title;
    }
    public void setSource(String source) {
        this.source = source;
    }
    public void setPoster(String poster) {
        this.poster = poster;
    }
}
