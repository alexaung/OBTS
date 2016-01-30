package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.myanmar.obts.PaymentOptionsActivity;
import com.myanmar.obts.RoutesActivity;
import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class RecyclerContactAdapter extends RecyclerView.Adapter<RecyclerContactAdapter.DataObjectHolder> {
    Context context;
    ArrayList<ContactRowItems> itemsList;
    private static MyClickListener myClickListener;
    private static TicketPackage ticketPackage;
    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView txtContactTitle;
        TextView txtContactSeatNo;
        EditText txtContactFullName;
        EditText txtContactAge;
        RadioGroup rdGender;
        RadioGroup rdIDType;
        EditText txtContactIDNumber;
        LinearLayout layoutTop;
        LinearLayout layoutContactBottom;
        String SeatLabel;
        Button btnBookingContinue;
        public DataObjectHolder(View itemView) {
            super(itemView);
            txtContactTitle = (TextView) itemView.findViewById(R.id.txtContactTitle);
            txtContactSeatNo = (TextView) itemView.findViewById(R.id.txtContactSeatNo);

            txtContactFullName = (EditText) itemView.findViewById(R.id.txtContactFullName);
            txtContactAge = (EditText) itemView.findViewById(R.id.txtContactAge);

            rdGender = (RadioGroup) itemView.findViewById(R.id.rdGender);
            rdIDType = (RadioGroup) itemView.findViewById(R.id.rdIDType);
            txtContactIDNumber = (EditText) itemView.findViewById(R.id.txtContactIDNumber);

            layoutTop=(LinearLayout)itemView.findViewById(R.id.layoutTop);
            layoutContactBottom=(LinearLayout)itemView.findViewById(R.id.layoutContactBottom);

            btnBookingContinue=(Button)itemView.findViewById(R.id.btnBookingContinue);
            btnBookingContinue.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Bundle b=new Bundle();
                    b.putParcelable("Package", ticketPackage);
                    Intent myIntent = new Intent(v.getContext(), PaymentOptionsActivity.class);
                    myIntent.putExtras(b);
                    v.getContext().startActivity(myIntent);
                    //Toast.makeText(v.getContext(),"Continue booking clicked", Toast.LENGTH_SHORT).show();
                }
            });

            itemView.setOnClickListener(this);

            SeatLabel= itemView.getResources().getString(R.string.seat);
        }

        @Override
        public void onClick(View v) {
            myClickListener.onItemClick(getAdapterPosition(), v);
        }
    }

    public void setOnItemClickListener(MyClickListener myClickListener) {
        this.myClickListener = myClickListener;
    }

    //private SparseBooleanArray selectedItems;
    public RecyclerContactAdapter(Context context, ArrayList<ContactRowItems> itemsList,TicketPackage _package) {
        this.context = context;
        this.itemsList = itemsList;
        ticketPackage=_package;
    }

    @Override
    public int getItemCount() {
        if (itemsList == null) {
            return 0;
        } else {
            return itemsList.size();
        }
    }

    @Override
    public DataObjectHolder onCreateViewHolder(ViewGroup parent,
                                               int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.contact_row, parent, false);

        DataObjectHolder dataObjectHolder = new DataObjectHolder(view);
        return dataObjectHolder;
    }


    @Override
    public void onBindViewHolder(DataObjectHolder rowViewHolder, int position) {

        if(position==0)
            rowViewHolder.layoutTop.setVisibility(View.VISIBLE);
        else
            rowViewHolder.layoutTop.setVisibility(View.GONE);
        if(position+1==itemsList.size())
            rowViewHolder.layoutContactBottom.setVisibility(View.VISIBLE);
        else rowViewHolder.layoutContactBottom.setVisibility(View.GONE);

        ContactRowItems items = itemsList.get(position);
        rowViewHolder.txtContactTitle.setText(items.getTitle());
        rowViewHolder.txtContactSeatNo.setText(rowViewHolder.SeatLabel+" "+ items.getSeatNo());
        rowViewHolder.txtContactFullName.setText(items.getFullName());
        rowViewHolder.txtContactAge.setText(items.getAge());
        //rowViewHolder.rdGender.setSe
        //rowViewHolder.rdGender.setSe
        rowViewHolder.txtContactIDNumber.setText(items.getIDNumber());

       //rowViewHolder.myBackground.setSelected(selectedItems.get(position, false));
    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }
}