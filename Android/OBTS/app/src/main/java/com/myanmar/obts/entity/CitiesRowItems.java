package com.myanmar.obts.entity;

import java.util.ArrayList;

import retrofit.Call;
import retrofit.http.GET;

/**
 * Created by minh on 25/11/2015.
 */

public class CitiesRowItems {
    private String CityId;
    private String  CityDesc;
    private String RegionId;
    private String RegionDesc;
    private String CountryId;
    private String CountryDesc;

    public String getCityId(){return CityId;}

    public void setCityId(String _CityId){CityId=_CityId;}
    
    public String getCityDesc() {
        return CityDesc;
    }

    public void setCityDesc(String _CityDesc) {
        this.CityDesc = _CityDesc;
    }

    public String getRegionId() {
        return RegionId;
    }

    public void setRegionId(String _RegionId) {
        this.RegionId = _RegionId;
    }

    public String getRegionDesc() {
        return RegionDesc;
    }

    public void setRegionDesc(String _RegionDesc) {
        this.RegionDesc = _RegionDesc;
    }

    public String getCountryId() {
        return CountryId;
    }

    public void setCountryId(String _CountryId) {
        this.CountryId = _CountryId;
    }

    public String getCountryDesc() {
        return CountryDesc;
    }

    public void setCountryDesc(String _CountryDesc) {
        this.CountryDesc = _CountryDesc;
    }
}
