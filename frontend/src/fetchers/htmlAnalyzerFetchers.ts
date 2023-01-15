const HTML_ANALYZER_PREFIX = 'htmlanalyzer';
const ANALYZE_HTML = `${HTML_ANALYZER_PREFIX}/analyze-html`;
const ANALYZE_URL = `${HTML_ANALYZER_PREFIX}/analyze-url`;
const FIX_HTML_ALL = `${HTML_ANALYZER_PREFIX}/fix-html-all`;

const getApiEndpoint = (url: string) => {
  return `${import.meta.env.VITE_API_ENDPOINT}${url}`;
};

enum ErrorLevelEnums {
  ERROR,
  WARNING,
  SEO,
  ACCESSIBILITY,
}

enum ErrorEnums {
  // Summary: A tag was not closed.
  TAG_NOT_CLOSED,
  //
  // Summary: A tag was not opened.
  TAG_NOT_OPENED,

  // Summary: There is a charset mismatch between stream and declared (META) encoding.
  CHARSET_MISMATCH,

  // Summary: An end tag was not required.
  END_TAG_NOT_REQUIRED,

  // Summary: An end tag is invalid at this position.
  END_TAG_INVALID,

  // Summary: The DOCTYPE declaration was invalid or not HTML5.
  DOCTYPE_INVALID,

  // Summary: HTML Tag is missing
  HTML_TAG_MISSING,

  // Summary: Head Tag is missing
  HEAD_TAG_MISSING,

  // Summary: Title Tag is missing
  TITLE_TAG_MISSING,

  // Summary: Body Tag is missing
  BODY_TAG_MISSING,

  // Summary: H1 Tag is missing
  H1_TAG_MISSING,

  // Summary:
  // Input type attribute is invalid.
  INPUT_TYPE_INVALID,

  // Summary: An attribute was duplicated.
  ATTRIBUTE_DUPLICATE,
}

type ErrorType = {
  errorLevel: ErrorLevelEnums;
  errorType: ErrorEnums;
  reason: string;
  solution: string;
  code: string;
  line: number;
  linePosition: number;
  sourceText: string;
  streamPosition: number;
};

// type HTMLAttributeType = {
//   name: string;
//   value: string;
// };

// type HTMLNodeType = {
//   name: string;
//   innerHTML: string;
//   innerText: string;
//   outerHTML: string;
//   attributes: HTMLAttributeType[];
// };

export type HtmlAnalyzerResponseType = {
  errors: ErrorType[];
  html: string;
  rate: number;
};

type Response<T> = {
  data: T;
  message: string;
  status: number;
};

export const getAnalyzeFromHtml = async (html: string): Promise<Response<HtmlAnalyzerResponseType>> => {
  const response = await fetch(getApiEndpoint(ANALYZE_HTML), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html }),
  });
  return await response.json();
};

export const getAnalyzeFromUrl = async (html: string): Promise<Response<HtmlAnalyzerResponseType>> => {
  const response = await fetch(getApiEndpoint(ANALYZE_URL), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html, isUrl: true }),
  });
  return await response.json();
};

export const getFixHtmlAll = async (html: string): Promise<Response<HtmlAnalyzerResponseType>> => {
  const response = await fetch(getApiEndpoint(FIX_HTML_ALL), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html }),
  });
  return await response.json();
};
