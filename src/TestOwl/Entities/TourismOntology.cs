using System;
using System.Collections.Generic;
using System.Text;

namespace TestOwl.Entities
{
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    [System.Xml.Serialization.XmlRoot(Namespace = "http://www.w3.org/2002/07/owl#", IsNullable = false)]
    public partial class Ontology
    {

        private OntologyPrefix[] prefixField;

        private OntologyAnnotation annotationField;

        private OntologyDeclaration[] declarationField;

        private OntologyEquivalentClasses[] equivalentClassesField;

        private OntologySubClassOfClass[][] subClassOfField;

        private OntologyDisjointClasses[] disjointClassesField;

        private OntologyClassAssertion[] classAssertionField;

        private OntologyObjectPropertyAssertion[] objectPropertyAssertionField;

        private OntologyDataPropertyAssertion[] dataPropertyAssertionField;

        private OntologySubObjectPropertyOfObjectProperty[][] subObjectPropertyOfField;

        private OntologyInverseObjectPropertiesObjectProperty[][] inverseObjectPropertiesField;

        private OntologyFunctionalObjectProperty[] functionalObjectPropertyField;

        private OntologyInverseFunctionalObjectProperty[] inverseFunctionalObjectPropertyField;

        private OntologySymmetricObjectProperty symmetricObjectPropertyField;

        private OntologyAsymmetricObjectProperty[] asymmetricObjectPropertyField;

        private OntologyTransitiveObjectProperty[] transitiveObjectPropertyField;

        private OntologyIrreflexiveObjectProperty[] irreflexiveObjectPropertyField;

        private OntologyObjectPropertyDomain[] objectPropertyDomainField;

        private OntologyObjectPropertyRange[] objectPropertyRangeField;

        private OntologyDataProperty[] subDataPropertyOfField;

        private OntologyFunctionalDataProperty[] functionalDataPropertyField;

        private OntologyDataPropertyDomain[] dataPropertyDomainField;

        private OntologyDataPropertyRange[] dataPropertyRangeField;

        private OntologyAnnotationAssertion[] annotationAssertionField;

        private string baseField;

        private string ontologyIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Prefix")]
        public OntologyPrefix[] Prefix
        {
            get
            {
                return prefixField;
            }
            set
            {
                prefixField = value;
            }
        }

        /// <remarks/>
        public OntologyAnnotation Annotation
        {
            get
            {
                return annotationField;
            }
            set
            {
                annotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Declaration")]
        public OntologyDeclaration[] Declaration
        {
            get
            {
                return declarationField;
            }
            set
            {
                declarationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("EquivalentClasses")]
        public OntologyEquivalentClasses[] EquivalentClasses
        {
            get
            {
                return equivalentClassesField;
            }
            set
            {
                equivalentClassesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Class", typeof(OntologySubClassOfClass[]), IsNullable = false)]
        public OntologySubClassOfClass[][] SubClassOf
        {
            get
            {
                return subClassOfField;
            }
            set
            {
                subClassOfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("DisjointClasses")]
        public OntologyDisjointClasses[] DisjointClasses
        {
            get
            {
                return disjointClassesField;
            }
            set
            {
                disjointClassesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ClassAssertion")]
        public OntologyClassAssertion[] ClassAssertion
        {
            get
            {
                return classAssertionField;
            }
            set
            {
                classAssertionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ObjectPropertyAssertion")]
        public OntologyObjectPropertyAssertion[] ObjectPropertyAssertion
        {
            get
            {
                return objectPropertyAssertionField;
            }
            set
            {
                objectPropertyAssertionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("DataPropertyAssertion")]
        public OntologyDataPropertyAssertion[] DataPropertyAssertion
        {
            get
            {
                return dataPropertyAssertionField;
            }
            set
            {
                dataPropertyAssertionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("ObjectProperty", typeof(OntologySubObjectPropertyOfObjectProperty[]), IsNullable = false)]
        public OntologySubObjectPropertyOfObjectProperty[][] SubObjectPropertyOf
        {
            get
            {
                return subObjectPropertyOfField;
            }
            set
            {
                subObjectPropertyOfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("ObjectProperty", typeof(OntologyInverseObjectPropertiesObjectProperty[]), IsNullable = false)]
        public OntologyInverseObjectPropertiesObjectProperty[][] InverseObjectProperties
        {
            get
            {
                return inverseObjectPropertiesField;
            }
            set
            {
                inverseObjectPropertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("FunctionalObjectProperty")]
        public OntologyFunctionalObjectProperty[] FunctionalObjectProperty
        {
            get
            {
                return functionalObjectPropertyField;
            }
            set
            {
                functionalObjectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("InverseFunctionalObjectProperty")]
        public OntologyInverseFunctionalObjectProperty[] InverseFunctionalObjectProperty
        {
            get
            {
                return inverseFunctionalObjectPropertyField;
            }
            set
            {
                inverseFunctionalObjectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologySymmetricObjectProperty SymmetricObjectProperty
        {
            get
            {
                return symmetricObjectPropertyField;
            }
            set
            {
                symmetricObjectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("AsymmetricObjectProperty")]
        public OntologyAsymmetricObjectProperty[] AsymmetricObjectProperty
        {
            get
            {
                return asymmetricObjectPropertyField;
            }
            set
            {
                asymmetricObjectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TransitiveObjectProperty")]
        public OntologyTransitiveObjectProperty[] TransitiveObjectProperty
        {
            get
            {
                return transitiveObjectPropertyField;
            }
            set
            {
                transitiveObjectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("IrreflexiveObjectProperty")]
        public OntologyIrreflexiveObjectProperty[] IrreflexiveObjectProperty
        {
            get
            {
                return irreflexiveObjectPropertyField;
            }
            set
            {
                irreflexiveObjectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ObjectPropertyDomain")]
        public OntologyObjectPropertyDomain[] ObjectPropertyDomain
        {
            get
            {
                return objectPropertyDomainField;
            }
            set
            {
                objectPropertyDomainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ObjectPropertyRange")]
        public OntologyObjectPropertyRange[] ObjectPropertyRange
        {
            get
            {
                return objectPropertyRangeField;
            }
            set
            {
                objectPropertyRangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("DataProperty", IsNullable = false)]
        public OntologyDataProperty[] SubDataPropertyOf
        {
            get
            {
                return subDataPropertyOfField;
            }
            set
            {
                subDataPropertyOfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("FunctionalDataProperty")]
        public OntologyFunctionalDataProperty[] FunctionalDataProperty
        {
            get
            {
                return functionalDataPropertyField;
            }
            set
            {
                functionalDataPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("DataPropertyDomain")]
        public OntologyDataPropertyDomain[] DataPropertyDomain
        {
            get
            {
                return dataPropertyDomainField;
            }
            set
            {
                dataPropertyDomainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("DataPropertyRange")]
        public OntologyDataPropertyRange[] DataPropertyRange
        {
            get
            {
                return dataPropertyRangeField;
            }
            set
            {
                dataPropertyRangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("AnnotationAssertion")]
        public OntologyAnnotationAssertion[] AnnotationAssertion
        {
            get
            {
                return annotationAssertionField;
            }
            set
            {
                annotationAssertionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string @base
        {
            get
            {
                return baseField;
            }
            set
            {
                baseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string ontologyIRI
        {
            get
            {
                return ontologyIRIField;
            }
            set
            {
                ontologyIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyPrefix
    {

        private string nameField;

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotation
    {

        private OntologyAnnotationAnnotationProperty annotationPropertyField;

        private OntologyAnnotationLiteral literalField;

        /// <remarks/>
        public OntologyAnnotationAnnotationProperty AnnotationProperty
        {
            get
            {
                return annotationPropertyField;
            }
            set
            {
                annotationPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyAnnotationLiteral Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotationAnnotationProperty
    {

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotationLiteral
    {

        private string datatypeIRIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string datatypeIRI
        {
            get
            {
                return datatypeIRIField;
            }
            set
            {
                datatypeIRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclaration
    {

        private OntologyDeclarationAnnotationProperty annotationPropertyField;

        private OntologyDeclarationObjectProperty objectPropertyField;

        private OntologyDeclarationClass classField;

        private OntologyDeclarationDataProperty dataPropertyField;

        private OntologyDeclarationNamedIndividual namedIndividualField;

        /// <remarks/>
        public OntologyDeclarationAnnotationProperty AnnotationProperty
        {
            get
            {
                return annotationPropertyField;
            }
            set
            {
                annotationPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyDeclarationObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyDeclarationClass Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }

        /// <remarks/>
        public OntologyDeclarationDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyDeclarationNamedIndividual NamedIndividual
        {
            get
            {
                return namedIndividualField;
            }
            set
            {
                namedIndividualField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclarationAnnotationProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclarationObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclarationClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclarationDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDeclarationNamedIndividual
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClasses
    {

        private OntologyEquivalentClassesClass[] classField;

        private OntologyEquivalentClassesDataHasValue dataHasValueField;

        private OntologyEquivalentClassesObjectSomeValuesFrom objectSomeValuesFromField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Class")]
        public OntologyEquivalentClassesClass[] Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }

        /// <remarks/>
        public OntologyEquivalentClassesDataHasValue DataHasValue
        {
            get
            {
                return dataHasValueField;
            }
            set
            {
                dataHasValueField = value;
            }
        }

        /// <remarks/>
        public OntologyEquivalentClassesObjectSomeValuesFrom ObjectSomeValuesFrom
        {
            get
            {
                return objectSomeValuesFromField;
            }
            set
            {
                objectSomeValuesFromField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesClass
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesDataHasValue
    {

        private OntologyEquivalentClassesDataHasValueDataProperty dataPropertyField;

        private OntologyEquivalentClassesDataHasValueLiteral literalField;

        /// <remarks/>
        public OntologyEquivalentClassesDataHasValueDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyEquivalentClassesDataHasValueLiteral Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesDataHasValueDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesDataHasValueLiteral
    {

        private string datatypeIRIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string datatypeIRI
        {
            get
            {
                return datatypeIRIField;
            }
            set
            {
                datatypeIRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesObjectSomeValuesFrom
    {

        private OntologyEquivalentClassesObjectSomeValuesFromObjectProperty objectPropertyField;

        private OntologyEquivalentClassesObjectSomeValuesFromClass classField;

        /// <remarks/>
        public OntologyEquivalentClassesObjectSomeValuesFromObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyEquivalentClassesObjectSomeValuesFromClass Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesObjectSomeValuesFromObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyEquivalentClassesObjectSomeValuesFromClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologySubClassOfClass
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClasses
    {

        private OntologyDisjointClassesAnnotation annotationField;

        private OntologyDisjointClassesClass[] classField;

        /// <remarks/>
        public OntologyDisjointClassesAnnotation Annotation
        {
            get
            {
                return annotationField;
            }
            set
            {
                annotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Class")]
        public OntologyDisjointClassesClass[] Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClassesAnnotation
    {

        private OntologyDisjointClassesAnnotationAnnotation annotationField;

        private OntologyDisjointClassesAnnotationAnnotationProperty annotationPropertyField;

        private string literalField;

        /// <remarks/>
        public OntologyDisjointClassesAnnotationAnnotation Annotation
        {
            get
            {
                return annotationField;
            }
            set
            {
                annotationField = value;
            }
        }

        /// <remarks/>
        public OntologyDisjointClassesAnnotationAnnotationProperty AnnotationProperty
        {
            get
            {
                return annotationPropertyField;
            }
            set
            {
                annotationPropertyField = value;
            }
        }

        /// <remarks/>
        public string Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClassesAnnotationAnnotation
    {

        private OntologyDisjointClassesAnnotationAnnotationAnnotationProperty annotationPropertyField;

        private string literalField;

        /// <remarks/>
        public OntologyDisjointClassesAnnotationAnnotationAnnotationProperty AnnotationProperty
        {
            get
            {
                return annotationPropertyField;
            }
            set
            {
                annotationPropertyField = value;
            }
        }

        /// <remarks/>
        public string Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClassesAnnotationAnnotationAnnotationProperty
    {

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClassesAnnotationAnnotationProperty
    {

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDisjointClassesClass
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertion
    {

        private OntologyClassAssertionObjectSomeValuesFrom objectSomeValuesFromField;

        private OntologyClassAssertionObjectSomeValuesFrom1[] objectIntersectionOfField;

        private OntologyClassAssertionClass classField;

        private OntologyClassAssertionNamedIndividual namedIndividualField;

        /// <remarks/>
        public OntologyClassAssertionObjectSomeValuesFrom ObjectSomeValuesFrom
        {
            get
            {
                return objectSomeValuesFromField;
            }
            set
            {
                objectSomeValuesFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("ObjectSomeValuesFrom", IsNullable = false)]
        public OntologyClassAssertionObjectSomeValuesFrom1[] ObjectIntersectionOf
        {
            get
            {
                return objectIntersectionOfField;
            }
            set
            {
                objectIntersectionOfField = value;
            }
        }

        /// <remarks/>
        public OntologyClassAssertionClass Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }

        /// <remarks/>
        public OntologyClassAssertionNamedIndividual NamedIndividual
        {
            get
            {
                return namedIndividualField;
            }
            set
            {
                namedIndividualField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFrom
    {

        private OntologyClassAssertionObjectSomeValuesFromObjectProperty objectPropertyField;

        private OntologyClassAssertionObjectSomeValuesFromClass classField;

        /// <remarks/>
        public OntologyClassAssertionObjectSomeValuesFromObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyClassAssertionObjectSomeValuesFromClass Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFromObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFromClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFrom1
    {

        private OntologyClassAssertionObjectSomeValuesFromObjectProperty1 objectPropertyField;

        private OntologyClassAssertionObjectSomeValuesFromClass1 classField;

        /// <remarks/>
        public OntologyClassAssertionObjectSomeValuesFromObjectProperty1 ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyClassAssertionObjectSomeValuesFromClass1 Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFromObjectProperty1
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionObjectSomeValuesFromClass1
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyClassAssertionNamedIndividual
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyAssertion
    {

        private OntologyObjectPropertyAssertionObjectProperty objectPropertyField;

        private OntologyObjectPropertyAssertionNamedIndividual[] namedIndividualField;

        /// <remarks/>
        public OntologyObjectPropertyAssertionObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("NamedIndividual")]
        public OntologyObjectPropertyAssertionNamedIndividual[] NamedIndividual
        {
            get
            {
                return namedIndividualField;
            }
            set
            {
                namedIndividualField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyAssertionObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyAssertionNamedIndividual
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyAssertion
    {

        private OntologyDataPropertyAssertionDataProperty dataPropertyField;

        private OntologyDataPropertyAssertionNamedIndividual namedIndividualField;

        private OntologyDataPropertyAssertionLiteral literalField;

        /// <remarks/>
        public OntologyDataPropertyAssertionDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyDataPropertyAssertionNamedIndividual NamedIndividual
        {
            get
            {
                return namedIndividualField;
            }
            set
            {
                namedIndividualField = value;
            }
        }

        /// <remarks/>
        public OntologyDataPropertyAssertionLiteral Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyAssertionDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyAssertionNamedIndividual
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyAssertionLiteral
    {

        private string datatypeIRIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string datatypeIRI
        {
            get
            {
                return datatypeIRIField;
            }
            set
            {
                datatypeIRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologySubObjectPropertyOfObjectProperty
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyInverseObjectPropertiesObjectProperty
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyFunctionalObjectProperty
    {

        private OntologyFunctionalObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologyFunctionalObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyFunctionalObjectPropertyObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyInverseFunctionalObjectProperty
    {

        private OntologyInverseFunctionalObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologyInverseFunctionalObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyInverseFunctionalObjectPropertyObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologySymmetricObjectProperty
    {

        private OntologySymmetricObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologySymmetricObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologySymmetricObjectPropertyObjectProperty
    {

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAsymmetricObjectProperty
    {

        private OntologyAsymmetricObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologyAsymmetricObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAsymmetricObjectPropertyObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyTransitiveObjectProperty
    {

        private OntologyTransitiveObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologyTransitiveObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyTransitiveObjectPropertyObjectProperty
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyIrreflexiveObjectProperty
    {

        private OntologyIrreflexiveObjectPropertyObjectProperty objectPropertyField;

        /// <remarks/>
        public OntologyIrreflexiveObjectPropertyObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyIrreflexiveObjectPropertyObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyDomain
    {

        private OntologyObjectPropertyDomainObjectProperty objectPropertyField;

        private OntologyObjectPropertyDomainClass[] objectUnionOfField;

        private OntologyObjectPropertyDomainClass1 classField;

        /// <remarks/>
        public OntologyObjectPropertyDomainObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Class", IsNullable = false)]
        public OntologyObjectPropertyDomainClass[] ObjectUnionOf
        {
            get
            {
                return objectUnionOfField;
            }
            set
            {
                objectUnionOfField = value;
            }
        }

        /// <remarks/>
        public OntologyObjectPropertyDomainClass1 Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyDomainObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyDomainClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyDomainClass1
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyRange
    {

        private OntologyObjectPropertyRangeObjectProperty objectPropertyField;

        private OntologyObjectPropertyRangeClass classField;

        /// <remarks/>
        public OntologyObjectPropertyRangeObjectProperty ObjectProperty
        {
            get
            {
                return objectPropertyField;
            }
            set
            {
                objectPropertyField = value;
            }
        }

        /// <remarks/>
        public OntologyObjectPropertyRangeClass Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyRangeObjectProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyObjectPropertyRangeClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataProperty
    {

        private string iRIField;

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyFunctionalDataProperty
    {

        private OntologyFunctionalDataPropertyDataProperty dataPropertyField;

        /// <remarks/>
        public OntologyFunctionalDataPropertyDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyFunctionalDataPropertyDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyDomain
    {

        private OntologyDataPropertyDomainDataProperty dataPropertyField;

        private OntologyDataPropertyDomainClass[] objectUnionOfField;

        private OntologyDataPropertyDomainClass1 classField;

        /// <remarks/>
        public OntologyDataPropertyDomainDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Class", IsNullable = false)]
        public OntologyDataPropertyDomainClass[] ObjectUnionOf
        {
            get
            {
                return objectUnionOfField;
            }
            set
            {
                objectUnionOfField = value;
            }
        }

        /// <remarks/>
        public OntologyDataPropertyDomainClass1 Class
        {
            get
            {
                return classField;
            }
            set
            {
                classField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyDomainDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyDomainClass
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyDomainClass1
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyRange
    {

        private OntologyDataPropertyRangeDataProperty dataPropertyField;

        private string[] dataOneOfField;

        private OntologyDataPropertyRangeDatatype datatypeField;

        /// <remarks/>
        public OntologyDataPropertyRangeDataProperty DataProperty
        {
            get
            {
                return dataPropertyField;
            }
            set
            {
                dataPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItem("Literal", IsNullable = false)]
        public string[] DataOneOf
        {
            get
            {
                return dataOneOfField;
            }
            set
            {
                dataOneOfField = value;
            }
        }

        /// <remarks/>
        public OntologyDataPropertyRangeDatatype Datatype
        {
            get
            {
                return datatypeField;
            }
            set
            {
                datatypeField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyRangeDataProperty
    {

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyDataPropertyRangeDatatype
    {

        private string abbreviatedIRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotationAssertion
    {

        private OntologyAnnotationAssertionAnnotationProperty annotationPropertyField;

        private string[] iRIField;

        private OntologyAnnotationAssertionLiteral literalField;

        /// <remarks/>
        public OntologyAnnotationAssertionAnnotationProperty AnnotationProperty
        {
            get
            {
                return annotationPropertyField;
            }
            set
            {
                annotationPropertyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("IRI")]
        public string[] IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }

        /// <remarks/>
        public OntologyAnnotationAssertionLiteral Literal
        {
            get
            {
                return literalField;
            }
            set
            {
                literalField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotationAssertionAnnotationProperty
    {

        private string abbreviatedIRIField;

        private string iRIField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string abbreviatedIRI
        {
            get
            {
                return abbreviatedIRIField;
            }
            set
            {
                abbreviatedIRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string IRI
        {
            get
            {
                return iRIField;
            }
            set
            {
                iRIField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2002/07/owl#")]
    public partial class OntologyAnnotationAssertionLiteral
    {

        private string datatypeIRIField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public string datatypeIRI
        {
            get
            {
                return datatypeIRIField;
            }
            set
            {
                datatypeIRIField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }
}
