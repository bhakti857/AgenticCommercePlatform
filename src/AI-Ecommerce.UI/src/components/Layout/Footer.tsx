export default function Footer() {
  return (
    <footer className="border-t border-muted bg-surface">
      <div className="mx-auto max-w-6xl px-4 py-6 text-center text-sm text-secondary sm:px-6 lg:px-8">
        <p>© {new Date().getFullYear()} AI Commerce Platform. Built with .NET, React &amp; an AI agent harness.</p>
      </div>
    </footer>
  );
}
