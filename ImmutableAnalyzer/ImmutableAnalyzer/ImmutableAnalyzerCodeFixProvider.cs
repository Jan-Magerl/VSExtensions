namespace ImmutableAnalyzer
{
    using System.Collections.Immutable;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ImmutableAnalyzerCodeFixProvider))]
    [Shared]
    public class ImmutableAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string TitleProperty = "Remove Seter";

        private const string TitleMethod = "Remove Method";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ImmutableAnalyzerAnalyzer.DiagnosticIdProperty, ImmutableAnalyzerAnalyzer.DiagnosticIdVoidMethod, ImmutableAnalyzerAnalyzer.DiagnosticIdMethod); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            if (diagnostic.Id == ImmutableAnalyzerAnalyzer.DiagnosticIdProperty)
            {
                var diagnosticSpan = diagnostic.Location.SourceSpan;
                var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<AccessorDeclarationSyntax>().First();
                context.RegisterCodeFix(CodeAction.Create(title: TitleProperty, createChangedDocument: c => RemoveSeter(context.Document, declaration, c), equivalenceKey: TitleProperty), diagnostic);
            }
            else
            {
                var diagnosticSpan = diagnostic.Location.SourceSpan;
                var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().First();

                context.RegisterCodeFix(CodeAction.Create(title: TitleMethod, createChangedDocument: c => RemoveMethod(context.Document, declaration, c), equivalenceKey: TitleMethod), diagnostic);
            }
        }

        private async Task<Document> RemoveSeter(Document document, AccessorDeclarationSyntax accessorDecl, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newroot = root.RemoveNode(accessorDecl, SyntaxRemoveOptions.KeepNoTrivia);
            return document.WithSyntaxRoot(newroot);
        }

        private async Task<Document> RemoveMethod(Document document, MethodDeclarationSyntax methodDecl, CancellationToken cancellationToken)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newroot = root.RemoveNode(methodDecl, SyntaxRemoveOptions.KeepNoTrivia);
            return document.WithSyntaxRoot(newroot);
        }
    }
}