using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace wpf_practical.classes
{
    public class AttributeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return ValidationResult.ValidResult;
        }

        //void GetParentBinding(BindingExpressionBase owner)
        //{
        //    string o = "";
        //    Type BEBfieldsType = owner.GetType();

        //    FieldInfo[] fields = BEBfieldsType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

        //    //PropertyPath pp = (PropertyPath)BEBfieldsType.GetField("_ppath", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).GetValue(owner);
        //    //MessageBox.Show(pp.Path);
        //    for (int i = 0; i < fields.Length; i++)
        //    {
        //        o += $"{fields[i].Name}, {fields[i].GetValue(owner)}" + '\n';
        //    }
        //    Clipboard.SetText(o);
        //    MessageBox.Show(o);

        //    string o2 = "";

        //}
        object GetValidatingObject(BindingExpressionBase owner)
        {
            Type BBType = owner.GetType();
            WeakReference weakReference = (WeakReference)BBType.GetField("_dataItem", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).GetValue(owner);
            return weakReference.Target;
        }
        string GetPropertyName(BindingBase owner)
        {
            Type BBType = owner.GetType();
            PropertyPath pp = (PropertyPath)BBType.GetField("_ppath", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).GetValue(owner);
            return pp.Path;
        }

        List<ValidationAttribute> GetValidationAttributes(object Object, string PropertyName)
        {
            Type OType = Object.GetType().BaseType;
            PropertyInfo fieldInfo = OType.GetProperty(PropertyName);
            object[] attributes = fieldInfo.GetCustomAttributes(true);
            List<ValidationAttribute> result = new List<ValidationAttribute>();
            foreach (object attribute in attributes)
            {
                if (attribute is ValidationAttribute)
                {
                    result.Add((ValidationAttribute)attribute);
                }
            }
            return result;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo, BindingExpressionBase owner)
        {

            object ValidatingObject = GetValidatingObject(owner);
            string PName = GetPropertyName(owner.ParentBindingBase);
            List<ValidationAttribute> validationAttributes = GetValidationAttributes(ValidatingObject, PName);
            foreach (ValidationAttribute validationAttribute in validationAttributes)
            {
                if (!validationAttribute.IsValid(value))
                {
                    return new ValidationResult(false, validationAttribute.ErrorMessage);
                }
            }
            //MessageBox.Show(GetPropertyName(owner.ParentBindingBase));


            return base.Validate(value, cultureInfo, owner);
        }
    }

}
