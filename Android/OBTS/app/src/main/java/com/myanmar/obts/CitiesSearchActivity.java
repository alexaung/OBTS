package com.myanmar.obts;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.EditText;

import com.myanmar.obts.entity.CitiesRowItems;
import com.myanmar.obts.entity.City;
import com.myanmar.obts.entity.DividerItemDecoration;
import com.myanmar.obts.entity.RecyclerCitiesAdapter;
import com.myanmar.obts.obts.R;
import com.myanmar.obts.service.ServiceGenerator;

import java.io.IOException;
import java.util.ArrayList;

import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class CitiesSearchActivity extends AppCompatActivity{

    RecyclerView recyclerView;
    ArrayList<CitiesRowItems> itemsList = new ArrayList<>();
    ArrayList<CitiesRowItems> itemsFilterList = new ArrayList<>();
    RecyclerCitiesAdapter adapter=null;
    ProgressDialog mDialog=null;

    private static String LOG_TAG = "RecyclerViewActivity";
    String Caller ="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cities_search);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        mDialog = ProgressDialog.show(CitiesSearchActivity.this, "",
                "Loading. Please wait...", true);
        /*
        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });
        */

        recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(CitiesSearchActivity.this));
        adapter = new RecyclerCitiesAdapter(CitiesSearchActivity.this, itemsList);
        recyclerView.setAdapter(adapter);

        RecyclerView.ItemDecoration itemDecoration =
                new DividerItemDecoration(CitiesSearchActivity.this, LinearLayoutManager.VERTICAL);
        recyclerView.addItemDecoration(itemDecoration);

        loadData();

        AppCompatButton btnBack=(AppCompatButton)findViewById(R.id.btnBack);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

        Bundle b = getIntent().getExtras();
        Caller = b.getString("Caller");


        EditText txtSearchText = (EditText)findViewById(R.id.txtSearchText);
        txtSearchText.addTextChangedListener(new TextWatcher() {

            @Override
            public void afterTextChanged(Editable s) {
                String txtEnter=s.toString();
                // TODO Auto-generated method stub
                //ArrayList<CitiesRowItems> list = (ArrayList<CitiesRowItems>)itemsList.clone();
                itemsFilterList =(ArrayList<CitiesRowItems>) itemsList.clone();
                boolean hasChange=false;
                for(int i=0;i<itemsList.size();i++)
                {
                    if (!itemsList.get(i).getCityDesc().toLowerCase().startsWith(txtEnter.toString().toLowerCase())) {
                        itemsFilterList.remove (itemsList.get(i));
                        hasChange=true;
                    }
                }
                if(hasChange) {
                    adapter.updateData(itemsFilterList);
                }

                if(txtEnter.trim().isEmpty()) adapter.updateData(itemsList);
            }

            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                // TODO Auto-generated method stub
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                //doSomething();
            }
        });
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.fadein, R.anim.fadeout);
    }

    @Override
    protected void onResume() {
        super.onResume();

        ((RecyclerCitiesAdapter) adapter).setOnItemClickListener(new
         RecyclerCitiesAdapter.MyClickListener() {
             @Override
             public void onItemClick(int position, View v) {
                 Log.i(LOG_TAG, " Clicked on Item " + position);
                 CitiesRowItems item = (CitiesRowItems) itemsFilterList.get(position);

                 Bundle b = new Bundle();
                 City city = new City(item.getCityId(), item.getCityDesc());
                 b.putParcelable("SelectedCity", city);
                 b.putString("Caller", Caller);
                 Intent intent = new Intent();
                 intent.putExtras(b);

                 setResult(RESULT_OK, intent);
                 finish();
             }
         });
    }

    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1) {
            if (resultCode == RESULT_OK) {
                finish();
            }
        }
    }

    public void loadData()
    {
        ServiceGenerator.CitiesService client = ServiceGenerator.createService(ServiceGenerator.CitiesService.class);//,"ncs\\minh","P@ssw0rd");
        // Fetch and print a list of the contributors to this library.
        Call<ArrayList<CitiesRowItems>> call=  client.getCities();
        call.enqueue(new Callback<ArrayList<CitiesRowItems>>() {
            @Override
            public void onResponse(Response<ArrayList<CitiesRowItems>> response, Retrofit retrofit) {
                if (response.isSuccess()) {
                    //Response success. Handle data here
                    itemsList = response.body();
                    adapter.updateData(itemsList);
                    itemsFilterList =(ArrayList<CitiesRowItems>) itemsList.clone();
                    mDialog.hide();
                } else {
                    //For getting error message
                    Log.d("Error message", response.raw().message().toString());
                    //For getting error code. Code is integer value like 200,404 etc
                    Log.d("Error code", String.valueOf(response.raw().code()));
                    mDialog.hide();

                    Bundle b=getIntent().getExtras();
                    b.putString("Message", response.raw().message().toString());

                    Intent myIntent = new Intent(CitiesSearchActivity.this, HandlerActivity.class);

                    myIntent.putExtras(b);
                    CitiesSearchActivity.this.startActivityForResult(myIntent, 1);
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

                Intent myIntent = new Intent(CitiesSearchActivity.this, HandlerActivity.class);

                myIntent.putExtras(b);
                CitiesSearchActivity.this.startActivityForResult(myIntent,1);
            }
        });
        /*
        ArrayList<CitiesRowItems> it = new ArrayList<CitiesRowItems>();
        CitiesRowItems items1 = new CitiesRowItems();
        items1.setTitle("Yangon");
        items1.setImgIcon(R.mipmap.city);
        items1.setId("1");
        it.add(items1);
        CitiesRowItems items2 = new CitiesRowItems();
        items2.setTitle("Mandalay");
        items2.setImgIcon(R.mipmap.city);
        items2.setId("2");
        it.add(items2);
        CitiesRowItems items3 = new CitiesRowItems();
        items3.setTitle("Taung Gyi");
        items3.setImgIcon(R.mipmap.city);
        items3.setId("3");
        it.add(items3);
        CitiesRowItems items4 = new CitiesRowItems();
        items4.setTitle("Maw La Myaing");
        items4.setImgIcon(R.mipmap.city);
        items4.setId("4");
        it.add(items4);
        CitiesRowItems items5 = new CitiesRowItems();
        items5.setTitle("Sit Dway");
        items5.setImgIcon(R.mipmap.city);
        items5.setId("5");
        it.add(items5);
        CitiesRowItems items6 = new CitiesRowItems();
        items6.setTitle("Pyay");
        items6.setImgIcon(R.mipmap.city);
        it.add(items6);
        items6.setId("6");
        CitiesRowItems items7 = new CitiesRowItems();
        items7.setTitle("Myeik");
        items7.setImgIcon(R.mipmap.city);
        items7.setId("7");
        it.add(items7);

        */
    }
}
