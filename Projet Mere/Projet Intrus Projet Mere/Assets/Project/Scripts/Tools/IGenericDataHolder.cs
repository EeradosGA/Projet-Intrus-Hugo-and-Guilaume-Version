using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectIntrus.Tools
{
    public interface IGenericDataHolder
    {
        Data GetData();
    }

    [System.Serializable]
    public class Data
    {
        public string this[string pKey]
        {
            set
            {
                GetInfo()[pKey] = value;
            }
            get { return GetInfo()[pKey]; }
        }

        public virtual DataInfo GetInfo()
        {
            DataInfo data = new DataInfo();
            data.Data = this;
            System.Reflection.FieldInfo[] infos = this.GetType().GetFields();
            for (int i = 0; i < infos.Length; i++)
            {
                System.Reflection.FieldInfo fieldInfo = this.GetType().GetField(infos[i].Name);
                data.Add(infos[i].Name, fieldInfo.GetValue(this).ToString(), fieldInfo.GetValue(this).GetType());
            }
            return data;
        }

        public virtual void SetInfo(DataInfo pData)
        {
            foreach (string key in pData.codes.Keys)
            {
                System.Reflection.FieldInfo info = this.GetType().GetField(key);
                info.SetValue(this, Convert.ChangeType(pData.codes[key].Value, info.GetValue(this).GetType()));
            }
        }

        public void AddToValue(string pKey, float pValue)
        {
            GetInfo().AddToValue(pKey, pValue);
        }

        public void UpdateData()
        {
            GetInfo().UpdateData();
        }
    }

    public struct DataCode
    {
        public string Value;
        public string Type;

        public DataCode(string pValue, string pType)
        {
            Value = pValue;
            Type = pType;
        }
    }

    public class DataInfo
    {
        public Data Data;
        public Dictionary<string, DataCode> codes = new Dictionary<string, DataCode>();
        public string this[string pKey]
        {
            set
            {
                DataCode code = this.codes[pKey];
                code.Value = value;
                this.codes[pKey] = code;
                UpdateData();
            }
            get { return this.codes[pKey].Value; }
        }

        public void AddToValue(string pKey, float pAddedValue)
        {
            if (codes.ContainsKey(pKey))
            {
                DataCode code = codes[pKey];
                Debug.Log("AddToValue : " + codes[pKey].Value);
                switch (codes[pKey].Type)
                {
                    case "Int32":
                        code.Value = ((Convert.ToInt32(codes[pKey].Value)) + pAddedValue).ToString();
                        break;
                    case "Int64":
                        code.Value = ((Convert.ToInt64(codes[pKey].Value)) + pAddedValue).ToString();
                        break;
                    case "Single":
                        code.Value = ((Convert.ToSingle(codes[pKey].Value)) + pAddedValue).ToString();
                        break;
                    case "Double":
                        code.Value = ((Convert.ToDouble(codes[pKey].Value)) + pAddedValue).ToString();
                        break;
                    default:
                        break;
                }
                codes[pKey] = code;
                UpdateData();

            }
        }

        public void SetValue(string pKey, string pValue)
        {
            if (codes.ContainsKey(pKey))
            {
                DataCode code = codes[pKey];
                code.Value = pValue;
                codes[pKey] = code;
                UpdateData();
            }
        }

        public string GetValue(string pKey)
        {
            if (codes.ContainsKey(pKey))
            {
                return codes[pKey].Value;
            }
            else
            {
                return null;
            }
        }

        public void UpdateData()
        {
            Data.SetInfo(this);
        }

        public void Add(string pKey, string pValue, Type type)
        {
            DataCode code = new DataCode(pValue, type.Name);
            codes.Add(pKey, code);
        }
    }
}