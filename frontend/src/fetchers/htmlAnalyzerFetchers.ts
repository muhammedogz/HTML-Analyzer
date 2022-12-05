const HTML_ANALYZER = 'htmlanalyzer';

const getApiEndpoint = (url: string) => {
  return `${import.meta.env.VITE_API_ENDPOINT}${url}`;
};

type HTMLAttributeType = {
  name: string;
  value: string;
};

// public HTMLError(string? code, string? reason, string? line, string? linePosition, string? sourceText, string? streamPosition)
//     {
//       Code = code;
//       Reason = reason;
//       Line = line;
//       LinePosition = linePosition;
//       SourceText = sourceText;
//       StreamPosition = streamPosition;
//     }

type ErrorType = {
  code: string;
  reason: string;
  line: number;
  linePosition: number;
  sourceText: string;
  streamPosition: number;
  solution: string;
};

type HTMLNodeType = {
  name: string;
  innerHTML: string;
  innerText: string;
  outerHTML: string;
  attributes: HTMLAttributeType[];
};

export type HTMlAnalyzerType = {
  errors: ErrorType[];
  html: string;
  description: string | null;
  title: string | null;
  text: string | null;
  htmlVersion: string | null;
  h1: HTMLNodeType[];
  h2: HTMLNodeType[];
  h3: HTMLNodeType[];
  h4: HTMLNodeType[];
  h5: HTMLNodeType[];
  h6: HTMLNodeType[];
  links: HTMLNodeType[];
  images: HTMLNodeType[];
  forms: HTMLNodeType[];
  scripts: HTMLNodeType[];
  styles: HTMLNodeType[];
  inputs: HTMLNodeType[];
  tables: HTMLNodeType[];
  meta: HTMLNodeType[];
  buttons: HTMLNodeType[];
  lists: HTMLNodeType[];
  divs: HTMLNodeType[];
  spans: HTMLNodeType[];
  paragraphs: HTMLNodeType[];
  headers: HTMLNodeType[];
  footers: HTMLNodeType[];
  navs: HTMLNodeType[];
  asides: HTMLNodeType[];
  sections: HTMLNodeType[];
  articles: HTMLNodeType[];
  main: HTMLNodeType[];
  details: HTMLNodeType[];
  summary: HTMLNodeType[];
  figures: HTMLNodeType[];
  figCaptions: HTMLNodeType[];
  iframes: HTMLNodeType[];
  labels: HTMLNodeType[];
  selects: HTMLNodeType[];
};

type Response<T> = {
  data: T;
  message: string;
  status: number;
};

export const getAnalyzeFromHtml = async (html: string): Promise<Response<HTMlAnalyzerType>> => {
  const response = await fetch(getApiEndpoint(HTML_ANALYZER), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html }),
  });
  return await response.json();
};

export const getAnalyzeFromUrl = async (html: string): Promise<Response<HTMlAnalyzerType>> => {
  const response = await fetch(getApiEndpoint(HTML_ANALYZER), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html, isUrl: true }),
  });
  return await response.json();
};
