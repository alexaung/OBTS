package com.myanmar.obts;

import android.app.ActionBar;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.InputType;
import android.text.Layout;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.myanmar.obts.entity.CitiesRowItems;
import com.myanmar.obts.entity.ContactRowItems;
import com.myanmar.obts.entity.DividerItemDecoration;
import com.myanmar.obts.entity.RecyclerCitiesAdapter;
import com.myanmar.obts.entity.RecyclerContactAdapter;
import com.myanmar.obts.entity.RecyclerDroppingPointAdapter;
import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class ContactDetailsActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    ArrayList<ContactRowItems> itemsList = new ArrayList<>();
    RecyclerContactAdapter adapter;

    private static String LOG_TAG = "RecyclerViewActivity";
    TicketPackage ticketPackage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_contact_details);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        Bundle b=getIntent().getExtras();
        ticketPackage=(TicketPackage)b.getParcelable("Package");

        TextView txtSeatTitle2=(TextView)findViewById(R.id.txtSeatTitle2);
        txtSeatTitle2.setText(ticketPackage.getRouteDate());

        recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        itemsList=getData();
        adapter = new RecyclerContactAdapter(ContactDetailsActivity.this,itemsList,ticketPackage);
        recyclerView.setAdapter(adapter);

        RecyclerView.ItemDecoration itemDecoration =
                new DividerItemDecoration(this, LinearLayoutManager.VERTICAL);
        recyclerView.addItemDecoration(itemDecoration);


        AppCompatButton btnBack=(AppCompatButton)findViewById(R.id.btnBack);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });


       // generateContactList(seats);
/*
        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });*/
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.fadein, R.anim.fadeout);
    }

    @Override
    protected void onResume() {
        super.onResume();
        ((RecyclerContactAdapter) adapter).setOnItemClickListener(new RecyclerContactAdapter.MyClickListener() {
            @Override
            public void onItemClick(int position, View v) {    }
        });
    }

    private ArrayList<ContactRowItems> getData(){
        ArrayList<ContactRowItems> it = new ArrayList<ContactRowItems>();
        String[] seats=ticketPackage.getSelectedSeats().split(",");
        for(int i=0;i<seats.length;i++) {
            ContactRowItems row=new ContactRowItems();

            row.setTitle((i==0?getResources().getString(R.string.primarypassenger):getResources().getString(R.string.copassenger)));
            row.setSeatNo(seats[i]);
            it.add(row);
        }
        return it;
    }

    private void generateContactList(String[] seats){

        RelativeLayout relativeLayout = (RelativeLayout) findViewById(R.id.contactLayout);
        RelativeLayout.LayoutParams p = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT,
                ViewGroup.LayoutParams.WRAP_CONTENT);
        for(int i=0;i<1;i++) {
            LinearLayout layout = new LinearLayout(this);
            p.addRule(RelativeLayout.BELOW, R.id.layoutTop);
            p.topMargin = 10;
            layout.setLayoutParams(p);
            layout.setBackgroundResource(R.drawable.group_box_bold);
            layout.setOrientation(LinearLayout.VERTICAL);
            layout.setPadding(16, 16, 16, 16);

            TableLayout tableLayout = new TableLayout(this);
            TableLayout.LayoutParams p3 = new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT, TableLayout.LayoutParams.WRAP_CONTENT);
            //p3.weight=0;
            tableLayout.setPadding(16, 16, 16, 16);
            tableLayout.setStretchAllColumns(true);
            tableLayout.setLayoutParams(p3);

            //title and seat number
            TableRow tableRow = new TableRow(this);
            TableRow.LayoutParams params = new TableRow.LayoutParams(
                    TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT);
            tableRow.setLayoutParams(params);

            //TableLayout.LayoutParams p1 = new TableLayout.LayoutParams(TableLayout.LayoutParams.WRAP_CONTENT,TableLayout.LayoutParams.WRAP_CONTENT);
            TextView textView = new TextView(this);

            if(i==0)
                textView.setText(R.string.primarypassenger);
            else
                textView.setText(R.string.copassenger);
            //params.gravity=Gravity.LEFT;
            textView.setGravity(Gravity.LEFT);
            textView.setLayoutParams(params);
            textView.setTypeface(textView.getTypeface(), Typeface.BOLD);
            tableRow.addView(textView);

            textView = new TextView(this);
            textView.setTag(R.string.seat);
            textView.setText(R.string.seat);
            textView.setGravity(Gravity.RIGHT);
            textView.setLayoutParams(params);
            textView.setTypeface(textView.getTypeface(), Typeface.BOLD);
            tableRow.addView(textView);


            tableLayout.addView(tableRow);
            layout.addView(tableLayout);

            //full name
            TextInputLayout textInputLayout = new TextInputLayout(this);
            TextInputLayout.LayoutParams params1 = new TextInputLayout.LayoutParams(TextInputLayout.LayoutParams.MATCH_PARENT, TextInputLayout.LayoutParams.WRAP_CONTENT);
            params1.topMargin = 8;
            params1.bottomMargin = 8;
            textInputLayout.setLayoutParams(params1);

            EditText editText = new EditText(this);
            editText.setTag(R.string.enter_full_name);
            editText.setLayoutParams(params1);
            editText.setHint(R.string.enter_full_name);

            textInputLayout.addView(editText);
            layout.addView(textInputLayout);

            //age
            textInputLayout = new TextInputLayout(this);
            editText = new EditText(this);
            editText.setTag(R.string.enter_age);
            editText.setInputType(InputType.TYPE_CLASS_NUMBER);
            editText.setLayoutParams(params1);
            editText.setHint(R.string.enter_age);

            textInputLayout.addView(editText);
            layout.addView(textInputLayout);

            //Gender
            RadioGroup.LayoutParams params2 = new RadioGroup.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
            RadioGroup radioGroup = new RadioGroup(this);
            radioGroup.setTag(R.string.gender);
            radioGroup.setOrientation(LinearLayout.HORIZONTAL);
            radioGroup.setLayoutParams(params2);

            textView = new TextView(this);
            textView.setText(R.string.gender);
            radioGroup.addView(textView);

            RadioButton radioButton = new RadioButton(this);
            radioButton.setText(R.string.male);
            radioGroup.addView(radioButton);
            radioButton = new RadioButton(this);
            radioButton.setText(R.string.female);
            radioGroup.addView(radioButton);
            layout.addView(radioGroup);

            //IC type
            params2 = new RadioGroup.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);
            radioGroup = new RadioGroup(this);
            radioGroup.setTag(R.string.ictype);
            radioGroup.setOrientation(LinearLayout.HORIZONTAL);
            radioGroup.setLayoutParams(params2);

            textView = new TextView(this);
            textView.setText(R.string.ictype);
            radioGroup.addView(textView);

            radioButton = new RadioButton(this);
            radioButton.setText(R.string.icnric);
            radioGroup.addView(radioButton);
            radioButton = new RadioButton(this);
            radioButton.setText(R.string.passport);
            radioGroup.addView(radioButton);
            layout.addView(radioGroup);

            //IC Number
            textInputLayout = new TextInputLayout(this);
            params1 = new TextInputLayout.LayoutParams(TextInputLayout.LayoutParams.MATCH_PARENT, TextInputLayout.LayoutParams.WRAP_CONTENT);
            params1.topMargin = 8;
            params1.bottomMargin = 8;
            textInputLayout.setLayoutParams(params1);

            editText = new EditText(this);
            editText.setTag(R.string.icnumber);
            editText.setLayoutParams(params1);
            editText.setHint(R.string.icnumber);

            textInputLayout.addView(editText);
            layout.addView(textInputLayout);

            relativeLayout.addView(layout);
            relativeLayout.invalidate();
        }

    }

}
