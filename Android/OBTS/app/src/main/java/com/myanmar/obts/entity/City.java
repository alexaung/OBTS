package com.myanmar.obts.entity;

import android.os.Parcel;
import android.os.Parcelable;

/**
 * Created by minh on 1/12/2015.
 */
public class City implements Parcelable {
    String Id;
    String Name;
    public String getName(){return Name;}
    public String getId(){return Id;}
    public void setName(String _Name){Name=_Name;}
    public void setId(String _Id){Id=_Id;}

    @Override
    public int describeContents() {
        // TODO Auto-generated method stub
        return 0;
    }


    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(Id);
        dest.writeString(Name);
    }

    private City(Parcel in){
        this.Id = in.readString();
        this.Name = in.readString();
    }

    public static final Parcelable.Creator<City> CREATOR = new Parcelable.Creator<City>() {

        @Override
        public City createFromParcel(Parcel source) {
            return new City(source);
        }

        @Override
        public City[] newArray(int size) {
            return new City[size];
        }
    };

    public City(String _Id,String _Name){
        this.Id = _Id;
        this.Name = _Name;
    }

}