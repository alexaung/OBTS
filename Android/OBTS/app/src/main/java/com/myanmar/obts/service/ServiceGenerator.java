package com.myanmar.obts.service;

import android.util.Base64;
import android.util.Log;

import com.myanmar.obts.entity.CitiesRowItems;
import com.myanmar.obts.entity.ContactDetails;
import com.myanmar.obts.entity.Passenger;
import com.myanmar.obts.entity.RoutePointsRowItems;
import com.myanmar.obts.entity.RoutesRowItems;
import com.squareup.okhttp.Authenticator;
import com.squareup.okhttp.Credentials;
import com.squareup.okhttp.Interceptor;
import com.squareup.okhttp.OkHttpClient;
import com.squareup.okhttp.Request;
import com.squareup.okhttp.Response;

import java.io.IOException;
import java.net.Proxy;
import java.net.SocketAddress;
import java.util.ArrayList;

import retrofit.Call;
import retrofit.GsonConverterFactory;
import retrofit.Retrofit;
import retrofit.http.Field;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.PUT;
import retrofit.http.Path;

/**
 * Created by minh on 7/12/2015.
 */
public class ServiceGenerator {
    public static final String API_BASE_URL = "http://192.168.0.141";

    private static OkHttpClient httpClient = new OkHttpClient();

    public interface CitiesService {
        @GET("/api/cities")
        public Call<ArrayList<CitiesRowItems>> getCities();
    }

    public interface RoutesService {
        @GET("/api/routes/{FromCity}/{ToCity}/{RouteDate}")
        public Call<ArrayList<RoutesRowItems>> getRoutes(@Path("FromCity") String FromCity,@Path("ToCity") String ToCity,@Path("RouteDate") String RouteDate);
    }

    public interface RoutesPointService {
       @GET("/api/route/{Id}/routepoints")
        public Call<ArrayList<RoutePointsRowItems>> getRoutePoints(@Path("Id") String Id);
    }

    public interface ContactDetailsService{
        @POST("/api/ContactDetails")
        Call<ContactDetails> insert(@Field("RouteId") String RouteId, @Field("Email") String Email, @Field("Mobile") String Mobile);
    }

    public interface PassengersService{
        @POST("/api/Passengers")
        Call<Passenger> insert(@Field("ContactDetailId") String ContactDetailId, @Field("FullName") String FullName,
                               @Field("Age") short Age, @Field("Gender") short Gender, @Field("IDType") short IDType,
                               @Field("IDNumber") String IDNumber, @Field("isPrimaryContact") boolean isPrimaryContact,
                               @Field("SeatNo") String SeatNo);
    }

    private static Retrofit.Builder builder =
            new Retrofit.Builder()
                    .baseUrl(API_BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create());

    public static <S> S createService(Class<S> serviceClass)//, String username, String password)
     {
         String username="ncs\\minh", password="P@ssw0rd";
        if (username != null && password != null) {
            String credentials = username + ":" + password;
            final String basic =
                    "Basic " + Base64.encodeToString(credentials.getBytes(), Base64.NO_WRAP);
            httpClient.interceptors().clear();
            httpClient.interceptors().add(new Interceptor() {
                @Override
                public Response intercept(Interceptor.Chain chain) throws IOException {
                    Request original = chain.request();

                    // try the request
                    Response response = chain.proceed(original);

                    int tryCount = 0;
                    while (!response.isSuccessful() && tryCount < 5) {

                        Log.d("intercept", "Request is not successful - " + tryCount);

                        tryCount++;

                        // retry the request
                        response = chain.proceed(original);
                    }


                    Request.Builder requestBuilder = original.newBuilder()
                            .header("Proxy-Authorization", basic)
                            .header("Accept", "applicaton/json")
                            .method(original.method(), original.body());

                    Request request = requestBuilder
                            .build();
                    return chain.proceed(request);
                }
            });
        }

        Retrofit retrofit = builder.client(httpClient).build();

        return retrofit.create(serviceClass);
    }


}
