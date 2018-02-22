﻿using SnackBarModel;
using SnackBarService.DataFromUser;
using SnackBarService.Interfaces;
using SnackBarService.ViewModel;
using System;
using System.Collections.Generic;

namespace SnackBarService.ImplementationsList
{
    public class ClientServiceList : InterfaceClientService
    {
        private DataListSingleton source;

        public ClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ModelClientView> getList()
        {
            List<ModelClientView> result = new List<ModelClientView>();
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                result.Add(new ModelClientView
                {
                    ID = source.Clients[i].ID,
                    ClientFullName = source.Clients[i].ClientFullName
                });
            }
            return result;
        }

        public ModelClientView getElement(int id)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].ID == id)
                {
                    return new ModelClientView
                    {
                        ID = source.Clients[i].ID,
                        ClientFullName = source.Clients[i].ClientFullName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void addElement(BoundClientModel model)
        {
            int maxID = 0;
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].ID > maxID)
                    maxID = source.Clients[i].ID;
                if (source.Clients[i].ClientFullName == model.ClientFullName)
                    throw new Exception("Уже есть клиент с таким ФИО");
            }
            source.Clients.Add(new Customer
            {
                ID = maxID + 1,
                ClientFullName = model.ClientFullName
            });
        }

        public void updateElement(BoundClientModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].ID == model.ID)
                    index = i;
                if (source.Clients[i].ClientFullName == model.ClientFullName && source.Clients[i].ID != model.ID)
                    throw new Exception("Уже есть клиент с таким ФИО");
            }
            if (index == -1)
                throw new Exception("Элемент не найден");
            source.Clients[index].ClientFullName = model.ClientFullName;
        }

        public void deleteElement(int id)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].ID == id)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
