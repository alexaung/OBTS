package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
public class DroppingPointsRowItems {
    private String title;
    private String  Id;

    public String getId(){return Id;}

    public void setId(String _Id){Id=_Id;}
    
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }
}
