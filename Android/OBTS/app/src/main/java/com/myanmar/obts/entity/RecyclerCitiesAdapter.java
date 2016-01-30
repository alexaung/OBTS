package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.TextView;

import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class RecyclerCitiesAdapter extends RecyclerView.Adapter<RecyclerCitiesAdapter.DataObjectHolder> {
    Context context;
    ArrayList<CitiesRowItems> itemsList;
    private static MyClickListener myClickListener;
    private int lastPosition = -1;

    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView title;
        ImageView image;

        public DataObjectHolder(View itemView) {
            super(itemView);
            title = (TextView) itemView.findViewById(R.id.title);
            image = (ImageView) itemView.findViewById(R.id.image);

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
    public RecyclerCitiesAdapter(Context context, ArrayList<CitiesRowItems> itemsList) {
        this.context = context;
        this.itemsList = (ArrayList<CitiesRowItems>)itemsList.clone();
    }

    public void updateData(ArrayList<CitiesRowItems> viewModels) {
        itemsList.clear();
        itemsList.addAll(viewModels);
        notifyDataSetChanged();
    }

    public void addItem(int position, CitiesRowItems viewModel) {
        itemsList.add(position, viewModel);
        notifyItemInserted(position);
    }

    public void removeItem(int position) {
        itemsList.remove(position);
        notifyItemRemoved(position);
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
        CitiesRowItems items = itemsList.get(position);
        rowViewHolder.title.setText(String.valueOf(items.getCityDesc()));
        //rowViewHolder.title.setTag(1, items.getCityId());
        rowViewHolder.image.setBackgroundResource(R.mipmap.city);

        setAnimation(((RecyclerView.ViewHolder) rowViewHolder).itemView, position);
       //rowViewHolder.myBackground.setSelected(selectedItems.get(position, false));
    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }

    private void setAnimation(View viewToAnimate, int position)
    {
        // If the bound view wasn't previously displayed on screen, it's animated
        if (position > lastPosition)
        {
            Animation animation = AnimationUtils.loadAnimation(context, android.R.anim.slide_in_left);
            viewToAnimate.startAnimation(animation);
            lastPosition = position;
        }
    }
}