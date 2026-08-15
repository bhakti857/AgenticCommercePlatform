import forms from '@tailwindcss/forms';

/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        // Design tokens — see README/design notes for usage ratios.
        // Page-level: bg ~78% / surface ~12% / primary ~6% / secondary ~3% / accent ~1%
        // Component-level (60/30/10): primary 60 / secondary 30 / accent 10
        bg: '#F7F8FA',
        surface: '#FFFFFF',
        primary: {
          DEFAULT: '#0F172A', // charcoal — headings, primary buttons
          hover: '#1E293B',
        },
        secondary: {
          DEFAULT: '#475569', // muted blue-gray — body text, secondary buttons
          hover: '#334155',
        },
        accent: {
          DEFAULT: '#06B6D4', // cyan — links, highlights, micro-interactions
          hover: '#0891B2',
        },
        muted: '#E6E9EE', // borders / dividers
      },
      fontFamily: {
        sans: ['system-ui', '-apple-system', 'Segoe UI', 'Roboto', 'sans-serif'],
      },
      borderRadius: {
        xl: '0.875rem',
        '2xl': '1.125rem',
      },
    },
  },
  plugins: [forms],
};
