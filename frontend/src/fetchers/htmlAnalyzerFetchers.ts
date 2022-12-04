const ENDPOINT = 'https://html-analyzer-backend.azurewebsites.net/'
const HTML_ANALYZER = 'htmlanalyzer'
const getApiEndpoint = (url: string) => {
  return `${ENDPOINT}${url}`
}

export const getAnalyzeFromHtml = async (html: string) => {
  const response = await fetch(getApiEndpoint(HTML_ANALYZER), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ html }),
  })
  return await response.json()
}