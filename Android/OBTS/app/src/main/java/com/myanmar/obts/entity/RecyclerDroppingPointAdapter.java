package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class RecyclerDroppingPointAdapter extends RecyclerView.Adapter<RecyclerDroppingPointAdapter.DataObjectHolder> {
    Context context;
    ArrayList<DroppingPointsRowItems> itemsList;
    private static MyClickListener myClickListener;

    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView title;

        public DataObjectHolder(View itemView) {
            super(itemView);
            title = (TextView) itemView.findViewById(R.id.title);

            itemView.setOnClickListener(this);
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
    public RecyclerDroppingPointAdapter(Context context, ArrayList<DroppingPointsRowItems> itemsList) {
        this.context = context;
        this.itemsList = itemsList;
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
                .inflate(R.layout.single_row, parent, false);

        DataObjectHolder dataObjectHolder = new DataObjectHolder(view);
        return dataObjectHolder;
    }


    @Override
    public void onBindViewHolder(DataObjectHolder rowViewHolder, int position) {
        DroppingPointsRowItems items = itemsList.get(position);
        rowViewHolder.title.setText(String.valueOf(items.getTitle()));

    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }
}