﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;

namespace Travelore.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public void InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                List<TravelList> travelLists = new List<TravelList>();
                TravelList t1 = new TravelList("Summer 2021", "Buthan", "Florstraat", "33A", DateTime.Now.AddDays(100), DateTime.Now.AddDays(110));
                TravelList t2 = new TravelList("#SummerIsLit", "Australië", "Blipblop", "1", DateTime.Now.AddDays(33), DateTime.Now.AddDays(66));
                TravelList t3 = new TravelList("Flor summer", "DorpLand", "Vlasbloemstraat", "3", DateTime.Now.AddDays(75), DateTime.Now.AddDays(80));

                travelLists.Add(t1);
                travelLists.Add(t2);
                travelLists.Add(t3);

                foreach (TravelList t in travelLists)
                    _context.TravelLists.Add(t);
                _context.SaveChanges();

                List<Category> categories = new List<Category>();
                Category c1 = new Category("Toiletries",false);
                Category c2 = new Category("Baby",false);
                Category c3 = new Category("Swimming",false);
                Category c4 = new Category("Supplies",false);
                Category c5 = new Category("Camping",false);
                Category c6 = new Category("Evening wear", false);
                categories.Add(c1);
                categories.Add(c2);
                categories.Add(c3);
                categories.Add(c4);
                categories.Add(c5);
                categories.Add(c6);

                foreach (Category c in categories)
                    _context.Categories.Add(c);
                _context.SaveChanges();

                t1.AddCategory(c1);
                t1.AddCategory(c2);
                t2.AddCategory(c3);
                t2.AddCategory(c4);
                t3.AddCategory(c5);
                t3.AddCategory(c6);
                _context.SaveChanges();

                // CATEGORY 1
                List<Item> items1 = new List<Item>();
                Item i1 = new Item("Cotton swabs", 30, false);
                Item i2 = new Item("Eau de Cologne", 1, false);
                Item i3 = new Item("Nail clippers", 1, true);
                Item i4 = new Item("Tweezers", 1, false);
                Item i5 = new Item("Anti-Spotstick", 1, true);
                Item i6 = new Item("Toothbrush", 1, false);
                Item i7 = new Item("Toothpaste", 1, false);
                Item i8 = new Item("Hairbrush", 1, true);
                items1.Add(i1);
                items1.Add(i2);
                items1.Add(i3);
                items1.Add(i4);
                items1.Add(i5);
                items1.Add(i6);
                items1.Add(i7);
                items1.Add(i8);

                foreach (Item c in items1)
                { 
                _context.Items.Add(c);
                c1.AddItem(c);
                }
                _context.SaveChanges();

                // CATEGORY 2
                List<Item> items2 = new List<Item>();
                Item i9 = new Item("Nappies", 1, false);
                Item i10 = new Item("Cushion change", 2, false);
                Item i11 = new Item("Blankets", 4, true);
                Item i12 = new Item("Plastic bags", 2, false);
                Item i13 = new Item("Baby cream", 1, true);
                Item i14 = new Item("Baby soap", 1, false);
                items2.Add(i9);
                items2.Add(i10);
                items2.Add(i11);
                items2.Add(i12);
                items2.Add(i13);
                items2.Add(i14);

                foreach (Item c in items2)
                {
                    _context.Items.Add(c);
                    c2.AddItem(c);
                }
                _context.SaveChanges();

                // CATEGORY 3
                List<Item> items3 = new List<Item>();
                Item i15 = new Item("Swimwear", 2, false);
                Item i16 = new Item("Swimming goggles", 1, false);
                Item i17 = new Item("Sandals", 1, true);
                items3.Add(i15);
                items3.Add(i16);
                items3.Add(i17);

                foreach (Item c in items3)
                {
                    _context.Items.Add(c);
                    c3.AddItem(c);
                }
                _context.SaveChanges();

                // CATEGORY 4
                List<Item> items4 = new List<Item>();
                Item i18 = new Item("Hand soap", 1, false);
                Item i19 = new Item("Lip balm", 2, false);
                Item i20 = new Item("Vitamins", 4, true);
                Item i21 = new Item("Underwear", 3, false);
                Item i22 = new Item("Socks", 10, true);
                Item i23 = new Item("Trousers", 10, false);
                items4.Add(i18);
                items4.Add(i19);
                items4.Add(i20);
                items4.Add(i21);
                items4.Add(i22);
                items4.Add(i23);

                foreach (Item c in items4)
                {
                    _context.Items.Add(c);
                    c4.AddItem(c);
                }
                _context.SaveChanges();

                // CATEGORY 5
                List<Item> items5 = new List<Item>();
                Item i24 = new Item("Tent", 1, false);
                Item i25 = new Item("Sleeping bag", 1, true);
                Item i26 = new Item("Sleeping mat", 1, false);
                Item i27 = new Item("Pillow", 4, true);
                Item i28 = new Item("Dining supplies", 10, false);
                items5.Add(i24);
                items5.Add(i25);
                items5.Add(i26);
                items5.Add(i27);
                items5.Add(i28);

                foreach (Item c in items5)
                {
                    _context.Items.Add(c);
                    c4.AddItem(c);
                }
                _context.SaveChanges();

                // CATEGORY 6
                List<Item> items6 = new List<Item>();
                Item i29 = new Item("Colbert", 1, false);
                Item i30 = new Item("Work shirt", 12, true);
                Item i31 = new Item("Work trousers", 1, false);
                Item i32 = new Item("Work shoes", 4, true);
                Item i33 = new Item("Work socks", 10, false);
                items6.Add(i29);
                items6.Add(i30);
                items6.Add(i31);
                items6.Add(i32);
                items6.Add(i33);

                foreach (Item c in items6)
                {
                    _context.Items.Add(c);
                    c4.AddItem(c);
                }
                _context.SaveChanges();
            }
        }
    }
}
