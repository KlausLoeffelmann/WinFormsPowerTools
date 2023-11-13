using Microsoft.DotNet.DesignTools.Serialization;
using System.CodeDom;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;

namespace WinForms.PowerTools.Controls.Designer;

public class BindingTypeConverterExtenderDesigner : CodeDomSerializer
{
    internal const string TemplateAssignmentNamespace = "WinForms.Tiles";

    public override object Serialize(
        IDesignerSerializationManager manager,
        object value)
    {
        if (!Debugger.IsAttached)
            Debugger.Launch();

        if (manager.Context.Current is ExpressionContext expressionContext)
        {
            // This is the left-side assignment target we want to generate.
            // And it describes the current context for which we need the
            // object generation. Like:
            // this.tileRepeater1.TemplateAssignmentProperty
            var contextExpression = expressionContext.Expression;

            CodeStatementCollection statements = new CodeStatementCollection();

            statements.AddRange(new CodeStatementCollection
            {
            });

            return statements;
        };

        var baseResult = base.Serialize(manager, value);

        return baseResult;
    }
}
