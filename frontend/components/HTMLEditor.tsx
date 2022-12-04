import { Button, FormLabel, Stack, useMediaQuery, useTheme } from "@mui/material";
import "ace-builds/src-noconflict/ext-language_tools";
import "ace-builds/src-noconflict/mode-html";
import "ace-builds/src-noconflict/theme-monokai";
import "prismjs/themes/prism-coy.css";
import { useCallback, useState } from "react";
import AceEditor from "react-ace";
import { getAnalyzeFromHtml } from "src/fetchers/htmlAnalyzerFetchers";

function HTMLEditor() {
  const isDesktop = useMediaQuery(useTheme().breakpoints.up("md"));
  const [code, setCode] = useState("<h1>Hello World!</h1>");

  const getHtmlFromUrl = useCallback(async (url: string) => {
    try {
      const response = await fetch(url);
      const text = await response.text();
      return text;
    } catch (error) {
      console.log(error);
    }
  }, []);

  const getHtml = useCallback(async () => {
    const html = await getHtmlFromUrl("https://www.google.com");
    if (html) {
      setCode(html);
    }
  }, [getHtmlFromUrl]);
  
  const sendHtml = useCallback(async () => {
    try {
      const response = await getAnalyzeFromHtml(code);
      console.log(response);
    } catch (error) {
      console.error(error);
      
    }
  }, [])

  // useEffect(() => {
  //   getHtml();
  // }, [getHtml]);


  return (
    <Stack>
      <FormLabel htmlFor="html-editor">Enter HTML Input</FormLabel>
      <Stack
        sx={{
          m: 2,
        }}
      >
        <AceEditor
          wrapEnabled
          placeholder="Enter your HTML"
          mode="html"
          theme="monokai"
          name="blah2"
          fontSize={14}
          showGutter={true}
          value={code}
          onChange={(value) => setCode(value)}
          onPaste={(value) => setCode(value)}
          setOptions={{
            enableBasicAutocompletion: true,
            enableLiveAutocompletion: false,
            enableSnippets: false,
            showLineNumbers: true,
            tabSize: 2,
          }}
          style={{
            width: isDesktop ? "700px" : "85vw",
          }}
        />
        <Button 
          onClick={sendHtml}
        >
          Send
        </Button>
      </Stack>
    </Stack>
  );
}

export default HTMLEditor;
