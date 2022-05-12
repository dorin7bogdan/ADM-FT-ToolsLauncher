﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.530
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class failure
{

    private string typeField;

    private string messageField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class error
{

    private string typeField;

    private string messageField;

    private string[] textField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class properties
{

    private property[] propertyField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("property")]
    public property[] property
    {
        get
        {
            return this.propertyField;
        }
        set
        {
            this.propertyField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class property
{

    private string nameField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class testcase
{

    private string skippedField;

    private List<error> errorField = new List<error>();

    private List<failure> failureField = new List<failure>();

    private string systemoutField;

    private string systemerrField;

    private string nameField;

    private string assertionsField;

    private string timeField;

    private string classnameField;

    private string statusField;

    private string startExecDt; // YYYY-MM-DD HH:mm:ss

    /// <remarks/>
    public string skipped
    {
        get
        {
            return this.skippedField;
        }
        set
        {
            this.skippedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("error")]
    public error[] error
    {
        get
        {
            return this.errorField.ToArray();
        }
        set
        {
            this.errorField.Clear();
            this.errorField.AddRange(value);
        }
    }


    public void AddError(error f)
    {
        errorField.Add(f);
    }

    public void AddFailure(failure f)
    {
        failureField.Add(f);
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("failure")]
    public failure[] failure
    {
        get
        {
            return this.failureField.ToArray();
        }
        set
        {
            this.failureField.Clear();
            this.failureField.AddRange(value);
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("system-out")]
    public string systemout
    {
        get
        {
            return this.systemoutField;
        }
        set
        {
            this.systemoutField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("system-err")]
    public string systemerr
    {
        get
        {
            return this.systemerrField;
        }
        set
        {
            this.systemerrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }


    /// <summary>
    /// special field added for tests
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute("type")]
    public string type { get; set; }

    /// <summary>
    /// special field added for tests
    /// </summary>
    [System.Xml.Serialization.XmlAttributeAttribute("report")]
    public string report { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string assertions
    {
        get
        {
            return this.assertionsField;
        }
        set
        {
            this.assertionsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string startExecDateTime
    {
        get { return this.startExecDt; }
        set { this.startExecDt = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string classname
    {
        get
        {
            return this.classnameField;
        }
        set
        {
            this.classnameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class testsuite
{

    private property[] propertiesField;

    private List<testcase> testcaseField = new List<testcase>();

    private string systemoutField;

    private string systemerrField;

    private string nameField;

    private string testsField;

    private string failuresField;

    private string errorsField;

    private string timeField;

    private string disabledField;

    private string skippedField;

    private string timestampField;

    private string hostnameField;

    private string idField;

    private string packageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("property", IsNullable = false)]
    public property[] properties
    {
        get
        {
            return this.propertiesField;
        }
        set
        {
            this.propertiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("testcase")]
    public testcase[] testcase
    {
        get
        {
            return this.testcaseField.ToArray();
        }
        set
        {
            testcaseField.Clear();
            testcaseField.AddRange(value);
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("system-out")]
    public string systemout
    {
        get
        {
            return this.systemoutField;
        }
        set
        {
            this.systemoutField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("system-err")]
    public string systemerr
    {
        get
        {
            return this.systemerrField;
        }
        set
        {
            this.systemerrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tests
    {
        get
        {
            return this.testsField;
        }
        set
        {
            this.testsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string failures
    {
        get
        {
            return this.failuresField;
        }
        set
        {
            this.failuresField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string errors
    {
        get
        {
            return this.errorsField;
        }
        set
        {
            this.errorsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string disabled
    {
        get
        {
            return this.disabledField;
        }
        set
        {
            this.disabledField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string skipped
    {
        get
        {
            return this.skippedField;
        }
        set
        {
            this.skippedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string timestamp
    {
        get
        {
            return this.timestampField;
        }
        set
        {
            this.timestampField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string hostname
    {
        get
        {
            return this.hostnameField;
        }
        set
        {
            this.hostnameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string package
    {
        get
        {
            return this.packageField;
        }
        set
        {
            this.packageField = value;
        }
    }

    internal void AddTestCase(testcase theTestCase)
    {
        testcaseField.Add(theTestCase);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class testsuites
{
    private List<testsuite> testsuiteField = new List<testsuite>();

    public void AddTestsuite(testsuite ts)
    {
        testsuiteField.Add(ts);
    }

    private string nameField;

    private string timeField;

    private string testsField;

    private string failuresField;

    private string disabledField;

    private string errorsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("testsuite")]
    public testsuite[] testsuite
    {
        get
        {
            return this.testsuiteField.ToArray();
        }
        set
        {
            this.testsuiteField.Clear();
            if (value != null)
                this.testsuiteField.AddRange(value);
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string time
    {
        get
        {
            return this.timeField;
        }
        set
        {
            this.timeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tests
    {
        get
        {
            return this.testsField;
        }
        set
        {
            this.testsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string failures
    {
        get
        {
            return this.failuresField;
        }
        set
        {
            this.failuresField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string disabled
    {
        get
        {
            return this.disabledField;
        }
        set
        {
            this.disabledField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string errors
    {
        get
        {
            return this.errorsField;
        }
        set
        {
            this.errorsField = value;
        }
    }

    internal void RemoveTestSuite(string name)
    {
        testsuite foundSuite = null;
        foreach (testsuite s in testsuiteField)
            if (s.name == name)
            {
                foundSuite = s;
                break;
            }

        if (foundSuite != null)
            testsuiteField.Remove(foundSuite);
    }
}
