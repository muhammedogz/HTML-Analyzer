namespace html_analyzer.Enums;

public enum ErrorEnums
{
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

  // Summary: Input type attribute is invalid.
  INPUT_TYPE_INVALID,

  // Summary: Image alt attribute is missing.
  IMAGE_ALT_MISSING,

  // Summary: Image src attribute is invalid.
  IMAGE_SRC_MISSING,

  // Summary: A href attribute is missing.
  HREF_MISSING,

  // Summary: Form action
  FORM_ACTION_MISSING,

  // Summary: Duplicate attributes
  DUPLICATE_ATTRIBUTES,

  // Summary: An attribute was duplicated.
  ATTRIBUTE_DUPLICATE,

  // Summary: Aria label is missing for an element.
  ARIA_LABEL_MISSING,

  // Summary: Meta is missing.
  META_MISSING,

}
