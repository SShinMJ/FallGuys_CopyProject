package com.example.fallguys.exception;

import lombok.AllArgsConstructor;
import lombok.Getter;
import org.springframework.http.HttpStatus;

@Getter
@AllArgsConstructor
public enum BaseResponseCode {

    /**
     * 200 OK : 요청 성공
     */
    OK(HttpStatus.OK, "요청 성공하였습니다."),

    /**
     * 400 BAD_REQUEST : 잘못된 요청
     */
    BAD_REQUEST(HttpStatus.BAD_REQUEST, "잘못된 요청입니다."),
    INVALID_PASSWORD(HttpStatus.BAD_REQUEST, "잘못된 비밀번호입니다. 다시 입력해주세요."),
    DUPLICATE_EMAIL(HttpStatus.BAD_REQUEST, "중복된 이메일입니다. 다시 입력해주세요."),
    DUPLICATE_NICKNAME(HttpStatus.BAD_REQUEST, "중복된 닉네임입니다. 다시 입력해주세요."),

    /**
     * 404 NOT FOUND
     */
    USER_NOT_FOUND(HttpStatus.NOT_FOUND, "사용자를 찾을 수 없습니다."),
    DISH_NOT_FOUND(HttpStatus.NOT_FOUND, "음식을 찾을 수 없습니다."),
    DISH_NUTRITION_FACTS_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 음식의 영양 성분 데이터를 찾을 수 없습니다."),
    COURSE_NOT_FOUND(HttpStatus.NOT_FOUND, "코스를 찾을 수 없습니다."),
    ROOM_NOT_FOUND(HttpStatus.NOT_FOUND, "객실을 찾을 수 없습니다."),
    ROOM_STATUS_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 객실 현황 데이터가 없습니다."),
    RESERVATION_NOT_FOUND(HttpStatus.NOT_FOUND, "예약 데이터가 없습니다."),
    RESERVATION_USER_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 사용자의 예약 데이터가 없습니다."),
    CART_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 장바구니 데이터가 없습니다."),
    ORDER_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 주문 내역이 없습니다."),
    QNA_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 게시글이 없습니다."),
    FAQ_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 게시글이 없습니다."),
    EVENT_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 게시글이 없습니다."),
    QNA_COMMENT_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 댓글이 없습니다."),
    REVIEW_NOT_FOUND(HttpStatus.NOT_FOUND, "해당 리뷰가 없습니다."),

    /**
     * 404 NOT FOUND
     */
    FAILED_TO_SAVE_USER(HttpStatus.NOT_FOUND, "사용자 등록에 실패했습니다."),
    FAILED_TO_SAVE_DISH(HttpStatus.NOT_FOUND, "음식 등록에 실패했습니다."),
    FAILED_TO_SAVE_ORDER_DISH_NUTRITION_FACTS(HttpStatus.NOT_FOUND, "음식 영양 성분 등록에 실패했습니다."),
    FAILED_TO_SAVE_COURSE(HttpStatus.NOT_FOUND, "코스 등록에 실패했습니다."),
    FAILED_TO_SAVE_ROOM(HttpStatus.NOT_FOUND, "객실 등록에 실패했습니다."),
    FAILED_TO_SAVE_ROOM_STATUS(HttpStatus.NOT_FOUND, "객실 현황 정보 등록에 실패했습니다."),
    FAILED_TO_SAVE_RESERVATION(HttpStatus.NOT_FOUND, "예약 정보 등록에 실패했습니다."),
    FAILED_TO_SAVE_CART(HttpStatus.NOT_FOUND, "장바구니 음식 등록에 실패했습니다."),
    FAILED_TO_SAVE_ORDER(HttpStatus.NOT_FOUND, "주문 등록에 실패했습니다."),
    FAILED_TO_SAVE_ORDER_DISH(HttpStatus.NOT_FOUND, "주문 음식 등록에 실패했습니다."),
    FAILED_TO_SAVE_QNA(HttpStatus.NOT_FOUND, "QNA 게시글 등록에 실패했습니다."),
    FAILED_TO_SAVE_FAQ(HttpStatus.NOT_FOUND, "FNQ 게시글 등록에 실패했습니다."),
    FAILED_TO_SAVE_EVENT(HttpStatus.NOT_FOUND, "EVENT 게시글 등록에 실패했습니다."),
    FAILED_TO_SAVE_COMMENT(HttpStatus.NOT_FOUND, "QNA 게시글의 댓글 등록에 실패했습니다."),
    FAILED_TO_SAVE_REVIEW(HttpStatus.NOT_FOUND, "리뷰 등록에 실패했습니다."),

    /**
     * 405 Method Not Allowed
     */
    METHOD_NOT_ALLOWED(HttpStatus.METHOD_NOT_ALLOWED, "해당 사용자에게 허용되지 않은 메서드입니다.");

    private HttpStatus httpStatus;
    private String message;
}
