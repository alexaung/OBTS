package com.myanmar.obts;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.AppCompatTextView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.myanmar.obts.entity.CitiesRowItems;
import com.myanmar.obts.entity.DividerItemDecoration;
import com.myanmar.obts.entity.RecyclerRoutesAdapter;
import com.myanmar.obts.entity.Route;
import com.myanmar.obts.entity.RoutesRowItems;
import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;
import com.myanmar.obts.service.ServiceGenerator;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;

import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class RoutesActivity extends AppCompatActivity {

    //String FromCity;String ToCity;
    TicketPackage ticketPackage;
    RecyclerView recyclerView;
    ArrayList<RoutesRowItems> itemsList = new ArrayList<>();
    ArrayList<RoutesRowItems> itemsFilterList = new ArrayList<>();
    RecyclerRoutesAdapter adapter;
    private RecyclerView.LayoutManager mLayoutManager;
    private static String LOG_TAG = "RecyclerViewActivity";
    ProgressDialog mDialog=null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_route_list);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        /*
        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });*/
        mDialog  = ProgressDialog.show(RoutesActivity.this, "",
                "Loading. Please wait...", true);

        Bundle b = getIntent().getExtras();
        ticketPackage =(TicketPackage) b.getParcelable("Package");

        AppCompatTextView txtRouteTitleBar=(AppCompatTextView)findViewById(R.id.txtRouteTitleBar);
        txtRouteTitleBar.setText(ticketPackage.getFromCity().getName() + " To "+ ticketPackage.getToCity().getName());

        TextView txtDate=(TextView)findViewById(R.id.txtDate);
        String[] sDate=ticketPackage.getRouteDate().split("-");
        Calendar cal=Calendar.getInstance();
        cal.set(Integer.valueOf(sDate[2]), Integer.valueOf(sDate[1]) - 1, Integer.valueOf(sDate[0]));
        String sDayName= new SimpleDateFormat("EEE").format(cal.getTime());
        String sMonthName=new SimpleDateFormat("MMM").format(cal.getTime());
        txtDate.setText(sDayName+","+sDate[0]+" "+sMonthName+" "+sDate[2]);

        recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        //itemsList=getData();
        adapter = new RecyclerRoutesAdapter(RoutesActivity.this,itemsList );
        recyclerView.setAdapter(adapter);

        RecyclerView.ItemDecoration itemDecoration =
                new DividerItemDecoration(this, LinearLayoutManager.VERTICAL);
        recyclerView.addItemDecoration(itemDecoration);

        loadData();

        AppCompatButton btnBack=(AppCompatButton)findViewById(R.id.btnBack);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

    }

    public  void onBusFareClick(View v)
    {
        AppCompatTextView btnDeparture=(AppCompatTextView)findViewById(R.id.btnDeparture);
        btnDeparture.setCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);

        AppCompatTextView btnFare=(AppCompatTextView)findViewById(R.id.btnFare);
        btnFare.setCompoundDrawablesWithIntrinsicBounds(R.mipmap.arrowup, 0, 0, 0);
    }

    public  void onDepartureClick(View v)
    {
        AppCompatTextView btnDeparture=(AppCompatTextView)findViewById(R.id.btnDeparture);
        btnDeparture.setCompoundDrawablesWithIntrinsicBounds(R.mipmap.arrowup, 0, 0, 0);

        AppCompatTextView btnFare=(AppCompatTextView)findViewById(R.id.btnFare);
        btnFare.setCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.fadein, R.anim.fadeout);
    }

    @Override
    protected void onResume() {
        super.onResume();
        ((RecyclerRoutesAdapter) adapter).setOnItemClickListener(new  RecyclerRoutesAdapter.MyClickListener()
        {
             @Override
             public void onItemClick(int position, View v) {
                 Log.i(LOG_TAG, " Clicked on Item " + position);
                 RoutesRowItems item = (RoutesRowItems) itemsFilterList.get(position);
                 //Intent intent = new Intent();
                 //intent.putExtra("selectedValue", item.getTitle());
                 //intent.putExtra("Caller", Caller);
                 //setResult(RESULT_OK, intent);
                 Toast.makeText(RoutesActivity.this, item.getCompany(), Toast.LENGTH_SHORT).show();
                 LoadSeats(item,v);
                 //finish();
             }
         });
    }

    private void LoadSeats(RoutesRowItems Route,View v){
        Intent myIntent = new Intent(RoutesActivity.this, SeatsActivity.class);
        Bundle b = new Bundle();
        ticketPackage.setRoute(new Route(null, null, null, null, 0, null, null,null));
        ticketPackage.getRoute().setCompany(Route.getCompany());
        ticketPackage.getRoute().setRouteId(Route.getRouteId());
        ticketPackage.getRoute().setDepartureTime(Route.getDepartureTime());
        ticketPackage.getRoute().setArrivalTime(Route.getArrivalTime());
        ticketPackage.getRoute().setPrice(Float.valueOf(Route.getRouteFare()));
        ticketPackage.getRoute().setCurrencyDesc(Route.getCurrencyDesc());
        ticketPackage.getRoute().setBusType(Route.getBusType());
        ticketPackage.getRoute().setseats(Route.seats);
        b.putParcelable("Package", ticketPackage);

        myIntent.putExtras(b);
        RoutesActivity.this.startActivity(myIntent);
    }

    public ArrayList<RoutesRowItems> loadData()
    {
        ArrayList<RoutesRowItems> it = new ArrayList<RoutesRowItems>();

        ServiceGenerator.RoutesService client = ServiceGenerator.createService(ServiceGenerator.RoutesService.class);//,"ncs\\minh","P@ssw0rd");
        // Fetch and print a list of the contributors to this library.
        Call<ArrayList<RoutesRowItems>> call=  client.getRoutes(ticketPackage.getFromCity().getId(), ticketPackage.getToCity().getId(), ticketPackage.getRouteDate());
        call.enqueue(new Callback<ArrayList<RoutesRowItems>>() {
            @Override
            public void onResponse(Response<ArrayList<RoutesRowItems>> response, Retrofit retrofit) {
                if (response.isSuccess()) {
                    //Response success. Handle data here
                    itemsList = response.body();
                    adapter.updateData(itemsList);
                    itemsFilterList =(ArrayList<RoutesRowItems>) itemsList.clone();
                    mDialog.hide();
                } else {
                    //For getting error message
                    Log.d("Error message", response.raw().message().toString());
                    //For getting error code. Code is integer value like 200,404 etc
                    Log.d("Error code", String.valueOf(response.raw().code()));
                    mDialog.hide();

                    Bundle b=getIntent().getExtras();
                    b.putString("Message", response.raw().message().toString());

                    Intent myIntent = new Intent(RoutesActivity.this, HandlerActivity.class);

                    myIntent.putExtras(b);
                    RoutesActivity.this.startActivityForResult(myIntent, 1);
                }
            }


            @Override
            public void onFailure(Throwable t) {
                if (t instanceof IOException) {
                    //Add your code for displaying no network connection error

                }
                mDialog.hide();
                Bundle b=getIntent().getExtras();
                b.putString("Message",t.getMessage());

                Intent myIntent = new Intent(RoutesActivity.this, HandlerActivity.class);

                myIntent.putExtras(b);
                RoutesActivity.this.startActivityForResult(myIntent,1);
            }
        });
        /*RoutesRowItems items1 = new RoutesRowItems();
        items1.setRouteId("1");
        items1.setDepartureTime("03:30 pm");
        items1.setArrivalTime("08:30 pm");
        items1.setRouteFare("20000");items1.setCurency("Ks");
        items1.setCompany("Mya Mar Lar");
        items1.setRouteDuration("12 hours");
        items1.setBusType("SVIP");
        items1.setTotalSeats("20 seats");
        it.add(items1);

        RoutesRowItems items2 = new RoutesRowItems();
        items2.setRouteId("2");
        items2.setDepartureTime("03:00 pm");
        items2.setArrivalTime("08:00 pm");
        items2.setRouteFare("20000");items2.setCurency("Ks");
        items2.setCompany("Shwe Min Thar");
        items2.setRouteDuration("12 hours");
        items2.setBusType("SVIP");
        items2.setTotalSeats("21 seats");
        it.add(items2);

        RoutesRowItems items3 = new RoutesRowItems();
        items3.setRouteId("3");
        items3.setDepartureTime("02:30 pm");
        items3.setArrivalTime("07:30 pm");
        items3.setRouteFare("20000");items3.setCurency("Ks");
        items3.setCompany("Kyal Sin");
        items3.setRouteDuration("11 hours");
        items3.setBusType("SVIP");
        items3.setTotalSeats("22 seats");
        it.add(items3);

        RoutesRowItems items4 = new RoutesRowItems();
        items4.setRouteId("4");
        items4.setDepartureTime("03:30 pm");
        items4.setArrivalTime("09:00 pm");
        items4.setRouteFare("20000");items4.setCurency("Ks");
        items4.setCompany("Mya Mar Lar");
        items4.setRouteDuration("10 hours");
        items4.setBusType("SVIP");
        items4.setTotalSeats("20 seats");
        it.add(items4);

        RoutesRowItems items5 = new RoutesRowItems();
        items5.setRouteId("5");
        items5.setDepartureTime("01:30 pm");
        items5.setArrivalTime("06:30 pm");
        items5.setRouteFare("20000");items5.setCurency("Ks");
        items5.setCompany("Aye Yar Myay");
        items5.setRouteDuration("12 hours");
        items5.setBusType("SVIP");
        items5.setTotalSeats("20 seats");
        it.add(items5);

        RoutesRowItems items6 = new RoutesRowItems();
        items6.setRouteId("6");
        items6.setDepartureTime("03:30 pm");
        items6.setArrivalTime("08:30 pm");
        items6.setRouteFare("20000");items6.setCurency("Ks");
        items6.setCompany("Nya Min Thar");
        items6.setRouteDuration("12 hours");
        items6.setBusType("SVIP");
        items6.setTotalSeats("20 seats");
        it.add(items6);

        RoutesRowItems items7 = new RoutesRowItems();
        items7.setRouteId("7");
        items7.setDepartureTime("03:30 pm");
        items7.setArrivalTime("08:30 pm");
        items7.setRouteFare("20000");
        items7.setCurency("Ks");
        items7.setCompany("Si Taw Gyi");
        items7.setRouteDuration("12 hours");
        items7.setBusType("SVIP");
        items7.setTotalSeats("20 seats");
        it.add(items7);*/
        return it;
    }
}
