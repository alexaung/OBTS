package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
public class ContactRowItems {
    private String title;
    private String SeatNo;
    private String  FullName;
    private String Age;
    private String Gender;
    private String IDType;
    private String IDNumber;

    public String getAge(){return Age;}

    public void setAge(String _Age){Age=_Age;}

    public String getIDNumber(){return IDNumber;}

    public void setIDNumber(String _IDNumber){IDNumber=_IDNumber;}

    public String getIDType(){return IDType;}

    public void setIDType(String _IDType){IDType=_IDType;}

    public String getGender(){return Gender;}

    public void setGender(String _Gender){Gender=_Gender;}

    public String getFullName(){return FullName;}

    public void setFullName(String _FullName){FullName=_FullName;}
    
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getSeatNo() {
        return SeatNo;
    }

    public void setSeatNo(String _SeatNo) {
        this.SeatNo = _SeatNo;
    }
}
