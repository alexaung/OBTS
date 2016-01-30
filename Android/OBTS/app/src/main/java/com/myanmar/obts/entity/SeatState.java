package com.myanmar.obts.entity;

/**
 * Created by minh on 30/11/2015.
 */
public enum SeatState {
    Available(3), NotApplicable(0), Sold(2), Space(1);
    private int value;

    private SeatState(int value) {
        this.value = value;
    }

    public int getValue(){return value;}
    public void setValue(int _value){value=_value;}
};
