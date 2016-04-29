namespace ImmutableAnalyzer
{
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ImmutableAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticIdProperty = "IMA001";
        public const string DiagnosticIdVoidMethod = "IMA002";
        public const string DiagnosticIdMethod = "IMA003";

        private const string Category = "Design";
        private const string ImmutableAttributeName = "ImmutableAttribute";

        private static readonly LocalizableString TitleProperty = new LocalizableResourceString(nameof(Resources.AnalyzerTitleProperty), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormatProperty = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormatProperty), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString DescriptionProperty = new LocalizableResourceString(nameof(Resources.AnalyzerDescriptionProperty), Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString TitleVoidMethod = new LocalizableResourceString(nameof(Resources.AnalyzerTitleVoidMethod), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormatVoidMethod = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormatVoidMethod), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString DescriptionVoidMethod = new LocalizableResourceString(nameof(Resources.AnalyzerDescriptionVoidMethod), Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString TitleMethod = new LocalizableResourceString(nameof(Resources.AnalyzerTitleMethod), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormatMethod = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormatMethod), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString DescriptionMethod = new LocalizableResourceString(nameof(Resources.AnalyzerDescriptionMethod), Resources.ResourceManager, typeof(Resources));

        private static DiagnosticDescriptor ruleProperty = new DiagnosticDescriptor(DiagnosticIdProperty, TitleProperty, MessageFormatProperty, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: DescriptionProperty);
        private static DiagnosticDescriptor ruleVoidMethod = new DiagnosticDescriptor(DiagnosticIdVoidMethod, TitleVoidMethod, MessageFormatVoidMethod, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: DescriptionVoidMethod);
        private static DiagnosticDescriptor ruleMethod = new DiagnosticDescriptor(DiagnosticIdMethod, TitleMethod, MessageFormatMethod, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: DescriptionMethod);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(ruleProperty, ruleVoidMethod, ruleMethod); }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeProperty, SymbolKind.Property);
            context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
        }

        private void AnalyzeProperty(SymbolAnalysisContext context)
        {
            var propertySymbol = (IPropertySymbol)context.Symbol;
            if (!propertySymbol.IsReadOnly)
            {
                if (propertySymbol.ContainingType.GetAttributes().Any(a => a.AttributeClass.Name == ImmutableAttributeName))
                {
                    var diagnostic = Diagnostic.Create(ruleProperty, propertySymbol.SetMethod.Locations[0], propertySymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        private void AnalyzeMethod(SymbolAnalysisContext context)
        {
            var methodSymbol = (IMethodSymbol)context.Symbol;
            if (methodSymbol.MethodKind == MethodKind.Ordinary)
            {
                if (methodSymbol.ReturnsVoid)
                {
                    if (methodSymbol.ContainingType.GetAttributes().Any(a => a.AttributeClass.Name == ImmutableAttributeName))
                    {
                        var diagnostic = Diagnostic.Create(ruleVoidMethod, methodSymbol.Locations[0], methodSymbol.Name);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
                else
                {
                    if (methodSymbol.ContainingType.GetAttributes().Any(a => a.AttributeClass.Name == ImmutableAttributeName))
                    {
                        var diagnostic = Diagnostic.Create(ruleMethod, methodSymbol.Locations[0], methodSymbol.Name);
                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
